namespace MetaCoins.Core.Entities
{
    public class CoinOwnerRecord
    {
        public Guid Id { get; set; }

        public Guid WalletId { get; set; }
        public Wallet Wallet { get; set; }
        
        public Guid CoinId { get; set; }
        public Coin Coin { get; set; }

        public DateTime AcquiredAt { get; set; }
    }
}