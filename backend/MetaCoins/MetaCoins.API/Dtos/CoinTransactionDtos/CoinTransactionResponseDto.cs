namespace MetaCoins.API.Dtos.CoinTransactionDtos
{
    public class CoinTransactionResponseDto
    {
        public Guid Id { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;

        public Guid CoinId { get; set; }
        public Guid? SenderWalletId { get; set; }
        public Guid? RecipientWalletId { get; set; }
        public Guid? CoinSellOrderId { get; set; }

        public string CreatedAt { get; set; } = string.Empty;
        public string UpdatedAt { get; set; } = string.Empty;
        public string? CompletedAt { get; set; } = string.Empty;
    }
}