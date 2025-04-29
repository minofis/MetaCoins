using MetaCoins.Core.Entities.Identity;

namespace MetaCoins.Core.Entities
{
    public class Wallet
    {
        public Guid Id { get; set; }   

        public List<Transaction> SentTransactions { get; set; } = new List<Transaction>();
        public List<Transaction> RecivedTransactions { get; set; }  = new List<Transaction>();
        public ICollection<Coin> Coins { get; set; }

        public Guid UserId { get; set; }
        public UserEntity? User { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}