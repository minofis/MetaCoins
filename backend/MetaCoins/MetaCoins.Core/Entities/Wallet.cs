using MetaCoins.Core.Entities.Identity;

namespace MetaCoins.Core.Entities
{
    public class Wallet
    {
        public Guid Id { get; set; }   

        public List<Transaction> SentTransactions { get; set; } 
        public List<Transaction> RecivedTransactions { get; set; } 
        public ICollection<Coin> Coins { get; set; }

        public Guid UserId { get; set; }
        public UserEntity User { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}