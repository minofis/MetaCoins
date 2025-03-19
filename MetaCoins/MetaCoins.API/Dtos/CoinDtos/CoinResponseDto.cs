namespace MetaCoins.API.Dtos.CoinDtos
{
    public class CoinResponseDto
    {
        public Guid Id { get; set; }
        public string ImageUrl { get; set; }
        public int LikesCount { get; set; }
        public string OwnerUsername { get; set; }
        public string CreatorUsername { get; set; }
        public string CreatedAt { get; set; }
    }
}