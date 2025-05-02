namespace MetaCoins.API.Dtos.CoinDtos
{
    public class CoinResponseDto
    {
        public Guid Id { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public string CoinStatus { get; set; } = string.Empty;
        public string Prompt { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int LikesCount { get; set; }
        public string OwnerUsername { get; set; } = string.Empty;
        public string CreatorUsername { get; set; } = string.Empty;
        public string CreatedAt { get; set; } = string.Empty;
    }
}