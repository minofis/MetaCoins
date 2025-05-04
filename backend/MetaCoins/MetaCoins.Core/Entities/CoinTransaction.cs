using MetaCoins.Core.Entities.Lookups.CoinTransaction;

namespace MetaCoins.Core.Entities
{
    public class CoinTransaction
    {
        public Guid Id { get; set; }

        public int TypeId { get; set; }
        public CoinTransactionType Type { get; set; } = null!;

        public int StatusId { get; set; } 
        public CoinTransactionStatus Status { get; set; } = null!;

        public Guid CoinId { get; set; }
        public Coin Coin { get; set; } = null!;

        public Guid? CoinSellOrderId { get; set; }
        public CoinSellOrder? CoinSellOrder { get; set; }

        public Guid? SenderWalletId { get; set; }
        public Wallet? SenderWallet { get; set; }

        public Guid? RecipientWalletId { get; set; }
        public Wallet? RecipientWallet { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
    }
}