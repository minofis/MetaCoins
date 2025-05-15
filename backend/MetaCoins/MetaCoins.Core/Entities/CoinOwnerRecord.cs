namespace MetaCoins.Core.Entities
{
    public class CoinOwnerRecord
    {
        public Guid Id { get; set; }

        public Guid WalletId { get; set; }
        public Wallet Wallet { get; set; } = null!;
        
        public Guid CoinId { get; set; }
        public Coin Coin { get; set; } = null!;

        public DateTime AcquiredAt { get; set; }
    }
}