using System.ComponentModel.DataAnnotations;

namespace MetaCoins.API.Dtos.UserDtos
{
    public class UserRegisterRequestDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}