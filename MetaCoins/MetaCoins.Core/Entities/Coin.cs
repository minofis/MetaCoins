using MetaCoins.Core.Entities.Voting;

namespace MetaCoins.Core.Entities
{
    public class Coin
    {
        public Guid Id { get; set; }
        public string ImageUrl { get; set; }
        public int LikesCount => Likes.Count;
        
        public Guid WalletId { get; set; }
        public Wallet Wallet { get; set; }

        public Guid CreatorId { get; set; }
        public Wallet Creator { get; set; }

        public List<Like> Likes { get; set; } = new();
        public List<Vote> Votes { get; set; } = new();

        public ICollection<CoinOwnerRecord> OwnershipRecords { get; set; } = new List<CoinOwnerRecord>();
        public DateTime CreatedAt { get; set; }
    }
}