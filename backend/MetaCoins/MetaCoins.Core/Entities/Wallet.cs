using MetaCoins.Core.Entities.Identity;

namespace MetaCoins.Core.Entities
{
    public class Wallet
    {
        public Guid Id { get; set; }   

        public ICollection<CoinTransaction> SentTransactions { get; set; } = new List<CoinTransaction>();
        public ICollection<CoinTransaction> RecivedTransactions { get; set; }  = new List<CoinTransaction>();
        public ICollection<Coin> Coins { get; set; } = new List<Coin>();

        public decimal Balance { get; set; } = 110;

        public Guid UserId { get; set; }
        public UserEntity User { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public bool HasEnoughBalance(decimal amount)
        {
            return Balance >= amount;
        }

        public void IncreaseBalance(decimal amount)
        {
            Balance += amount;
        }

        public void DecreaseBalance(decimal amount)
        {
            if (!HasEnoughBalance(amount))
                throw new InvalidOperationException($"Wallet with ID {Id} does not have enough balance.");
                
            Balance -= amount;
        }
    }
}