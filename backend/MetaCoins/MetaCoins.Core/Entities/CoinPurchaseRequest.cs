using MetaCoins.Core.Entities.Lookups.CoinPurchaseRequest;

namespace MetaCoins.Core.Entities
{
    public class CoinPurchaseRequest
    {
        public Guid Id { get; set; }

        public int StatusId { get; set; }
        public CoinPurchaseRequestStatus Status { get; set; } = null!;

        public Guid CoinSellOrderId { get; set; }
        public CoinSellOrder CoinSellOrder { get; set; } = null!;

        public Guid BuyerWalletId { get; set; }
        public Wallet BuyerWallet { get; set; } = null!;

        public DateTime CreatedAt { get; set; }
        public DateTime? RespondedAt { get; set; }
    }
}