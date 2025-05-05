using MetaCoins.Core.Entities.Identity;

namespace MetaCoins.Core.Entities
{
    public class Wallet
    {
        public Guid Id { get; set; }   

        public ICollection<CoinTransaction> SentTransactions { get; set; } = new List<CoinTransaction>();
        public ICollection<CoinTransaction> RecivedTransactions { get; set; }  = new List<CoinTransaction>();
        public ICollection<Coin> Coins { get; set; } = new List<Coin>();

        public decimal Balance { get; set; }

        public Guid UserId { get; set; }
        public UserEntity User { get; set; } = null!;

        public DateTime CreatedAt { get; set; }
    }
}