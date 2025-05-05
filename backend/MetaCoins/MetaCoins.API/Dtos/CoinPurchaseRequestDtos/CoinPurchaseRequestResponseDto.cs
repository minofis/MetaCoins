namespace MetaCoins.API.Dtos.CoinPurchaseRequestDtos
{
    public class CoinPurchaseRequestResponseDto
    {
        public Guid Id { get; set; }
        public string Status { get; set; } = string.Empty;
        public Guid CoinSellOrderId { get; set; }
        public Guid BuyerWalletId { get; set; }
        public string CreatedAt { get; set; } = string.Empty;
        public string? RespondedAt { get; set; } = string.Empty;
    }
}