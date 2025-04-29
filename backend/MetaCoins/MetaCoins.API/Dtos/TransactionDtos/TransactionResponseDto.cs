namespace MetaCoins.API.Dtos.TransactionDtos
{
    public class TransactionResponseDto
    {
        public Guid Id { get; set; }
        public Guid CoinId { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string CreatedAt { get; set; } = string.Empty;
        public Guid SenderWalletId { get; set; }
        public Guid RecipientWalletId { get; set; }
    }
}