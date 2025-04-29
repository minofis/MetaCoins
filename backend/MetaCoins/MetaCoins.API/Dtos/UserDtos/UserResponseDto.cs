namespace MetaCoins.API.Dtos.UserDtos
{
    public class UserResponseDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<RoleResponseDto> Roles { get; set; } = new List<RoleResponseDto>();
    }
}