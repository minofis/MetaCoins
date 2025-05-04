namespace MetaCoins.API.Dtos.CoinSellOrderDtos
{
    public class CoinSellOrderResponseDto
    {
        public Guid Id { get; set; }

        public string Status { get; set; } = string.Empty;

        public Guid SellerWalletId { get; set; }
        public Guid CoinId { get; set; }
        public decimal Price { get; set; }

        public string CreatedAt { get; set; } = string.Empty;
        public string UpdatedAt { get; set; } = string.Empty;
        public string? CompletedAt { get; set; } = string.Empty;
    }
}