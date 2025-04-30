using MetaCoins.Core.Entities.Identity;

namespace MetaCoins.Core.Entities.Votes
{
    public class CoinVote
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public UserEntity User { get; set; } = null!;

        public Guid CoinId { get; set; }
        public Coin Coin { get; set; } = null!;

        public Guid VotingSessionId { get; set; }
        public VotingSession VotingSession { get; set; } = null!;

        public DateTime CreatedAt { get; set; }
    }
}