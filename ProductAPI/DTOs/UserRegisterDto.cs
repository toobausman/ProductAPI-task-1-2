using System.ComponentModel.DataAnnotations;

namespace ProductAPI.DTOs
{
    public class UserRegisterDto
    {
        [Required, StringLength(100)]
        public string Username { get; set; } = string.Empty;

        [Required, MinLength(6)]
        public string Password { get; set; } = string.Empty;
    }
}
