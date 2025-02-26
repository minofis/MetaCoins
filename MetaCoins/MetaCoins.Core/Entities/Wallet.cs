using MetaCoins.Core.Entities.Identity;
using MetaCoins.Core.Entities.Lookups.Wallet;

namespace MetaCoins.Core.Entities
{
    public class Wallet
    {
        public Guid Id { get; set; }   

        public List<Transaction> SentTransactions { get; set; } 
        public List<Transaction> RecivedTransactions { get; set; } 
        public ICollection<Coin> Coins { get; set; }

        public int StatusId { get; set; } 
        public WalletStatus Status { get; set; }

        public Guid UserId { get; set; }
        public UserEntity User { get; set; }

        public WalletDetails Details { get; set; }
    }
}