using MetaCoins.Core.Entities.Lookups.CoinSellOrder;

namespace MetaCoins.Core.Entities
{
    public class CoinSellOrder
    {
        public Guid Id { get; set; }

        public int StatusId { get; set; }
        public CoinSellOrderStatus Status { get; set; } = null!;

        public Guid SellerWalletId { get; set; }
        public Wallet SellerWallet { get; set; } = null!;

        public Guid CoinId { get; set; }
        public Coin Coin { get; set; } = null!;

        public decimal Price { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
    }
}