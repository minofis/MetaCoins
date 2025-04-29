namespace MetaCoins.API.Dtos.LikeDtos
{
    public class LikeResponseDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public Guid CoinId { get; set; }
        public string CreatedAt { get; set; } = string.Empty;
    }
}