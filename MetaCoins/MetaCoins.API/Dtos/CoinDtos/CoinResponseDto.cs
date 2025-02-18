namespace MetaCoins.API.Dtos.CoinDtos
{
    public class CoinResponseDto
    {
        public Guid Id { get; set; }
        public decimal Value { get; set; }
        public Guid WalletId { get; set; }
        public Guid CreatorId { get; set; }
        public string CreatedAt { get; set; }
    }
}