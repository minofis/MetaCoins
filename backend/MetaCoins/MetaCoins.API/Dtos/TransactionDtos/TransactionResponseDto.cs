namespace MetaCoins.API.Dtos.TransactionDtos
{
    public class TransactionResponseDto
    {
        public Guid Id { get; set; }
        public Guid CoinId { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public string CreatedAt { get; set; }
        public Guid SenderWalletId { get; set; }
        public Guid RecipientWalletId { get; set; }
    }
}