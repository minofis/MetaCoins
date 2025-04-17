namespace MetaCoins.API.Dtos.CoinDtos
{
    public class CoinOwnerRecordResponseDto
    {
        public Guid Id { get; set; }
        public string OwnerUsername { get; set; }
        public Guid CoinId { get; set; }
        public string AcquiredAt { get; set; }
    }
}