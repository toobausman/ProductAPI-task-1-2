using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProductAPI.DTOs;
using ProductAPI.Models;
using ProductAPI.Repositories;
using ProductAPI.Results;

namespace ProductAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _users;
        private readonly IConfiguration _config;

        public AuthService(IUserRepository users, IConfiguration config)
        {
            _users = users;
            _config = config;
        }

        public async Task<ApiResult<object>> RegisterAsync(UserRegisterDto dto)
        {
            // rule: username must be unique
            if (await _users.UsernameExistsAsync(dto.Username))
                return ApiResult<object>.Conflict();

            var user = new User
            {
                Username = dto.Username.Trim(),
                PasswordHash = Hash(dto.Password)
            };

            await _users.AddAsync(user);

            // 201 Created (no Location header needed)
            return ApiResult<object>.Created(new { username = user.Username });
        }

        public async Task<ApiResult<object>> LoginAsync(UserLoginDto dto)
        {
            var user = await _users.GetByUsernameAsync(dto.Username);
            if (user is null || user.PasswordHash != Hash(dto.Password))
                return ApiResult<object>.Unauthorized();

            var token = CreateToken(user);
            return ApiResult<object>.Ok(new { token });
        }

        // -------- helpers --------
        private static string Hash(string input)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(input));
            return Convert.ToHexString(bytes);
        }

        private string CreateToken(User user)
        {
            var issuer = _config["Jwt:Issuer"];
            var audience = _config["Jwt:Audience"];
            var key = _config["Jwt:Key"];
            var creds = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key!)),
                SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(int.Parse(_config["Jwt:ExpireMinutes"] ?? "120")),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
