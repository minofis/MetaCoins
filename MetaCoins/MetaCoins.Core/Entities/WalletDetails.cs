namespace MetaCoins.Core.Entities
{
    public class WalletDetails
    {
        public Guid WalletId { get; set; }
        public Wallet Wallet { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}