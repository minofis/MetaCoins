using System.Text.Json.Serialization;
using MetaCoins.Core.Entities.Votes;

namespace MetaCoins.Core.Entities
{
    public class Coin
    {
        public Guid Id { get; set; }
        public string ImageUrl { get; set; } = string.Empty;

        public Guid WalletId { get; set; }
        public Wallet Wallet { get; set; } = null!;

        public Guid CreatorId { get; set; }
        public Wallet Creator { get; set; } = null!;

        public int LikesCount => Likes.Count;
        public ICollection<Like> Likes { get; set; } = new List<Like>();

        public ICollection<CoinOwnerRecord> OwnershipRecords { get; set; } = new List<CoinOwnerRecord>();
        public DateTime CreatedAt { get; set; }

        [JsonIgnore]
        public ICollection<CoinVotingSession> CoinVotingSessions { get; set; } = new List<CoinVotingSession>();
    }
}