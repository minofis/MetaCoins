namespace MetaCoins.API.Dtos.ProfileDtos
{
    public class ProfileResponseDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}