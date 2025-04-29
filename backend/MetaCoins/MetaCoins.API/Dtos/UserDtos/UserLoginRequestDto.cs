using System.ComponentModel.DataAnnotations;

namespace MetaCoins.API.Dtos.UserDtos
{
    public class UserLoginRequestDto
    {
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}