using ProductAPI.DTOs;
using ProductAPI.Results;

namespace ProductAPI.Services
{
    public interface IAuthService
    {
        Task<ApiResult<object>> RegisterAsync(UserRegisterDto dto);
        Task<ApiResult<object>> LoginAsync(UserLoginDto dto);
    }
}
