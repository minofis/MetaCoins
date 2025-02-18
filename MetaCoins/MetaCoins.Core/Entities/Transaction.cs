using MetaCoins.Core.Entities.Lookups.Transaction;

namespace MetaCoins.Core.Entities
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }

        public int TypeId { get; set; }
        public TransactionType Type { get; set; }

        public int StatusId { get; set; } 
        public TransactionStatus Status { get; set; }

        public Guid CoinId { get; set; }
        public Coin Coin { get; set; }

        public Guid SenderWalletId { get; set; }
        public Wallet SenderWallet { get; set; }

        public Guid RecipientWalletId { get; set; }
        public Wallet RecipientWallet { get; set; }
    }
}