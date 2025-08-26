using Microsoft.AspNetCore.Mvc;
using ProductAPI.DTOs;
using ProductAPI.Results;
using ProductAPI.Services;

namespace ProductAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth;
        public AuthController(IAuthService auth) => _auth = auth;

        /// Register a new user.
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDto dto)
            => (await _auth.RegisterAsync(dto)).ToActionResult(this);

        /// Login and receive a JWT bearer token.
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto dto)
            => (await _auth.LoginAsync(dto)).ToActionResult(this);
    }
}
