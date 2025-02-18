namespace MetaCoins.API.Dtos.UserDtos
{
    public class UserResponseDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public List<RoleResponseDto> Roles { get; set; }
    }
}