namespace MetaCoins.API.Dtos.CoinDtos
{
    public class CoinOwnerRecordResponseDto
    {
        public Guid Id { get; set; }
        public string OwnerUsername { get; set; } = string.Empty;
        public Guid CoinId { get; set; }
        public string AcquiredAt { get; set; } = string.Empty;
    }
}