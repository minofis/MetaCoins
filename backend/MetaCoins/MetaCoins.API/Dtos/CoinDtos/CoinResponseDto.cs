namespace MetaCoins.API.Dtos.CoinDtos
{
    public class CoinResponseDto
    {
        public Guid Id { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public int LikesCount { get; set; }
        public string OwnerUsername { get; set; } = string.Empty;
        public string CreatorUsername { get; set; } = string.Empty;
        public string CreatedAt { get; set; } = string.Empty;
    }
}