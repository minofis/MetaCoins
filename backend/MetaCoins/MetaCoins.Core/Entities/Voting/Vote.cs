using MetaCoins.Core.Entities.Identity;

namespace MetaCoins.Core.Entities.Voting
{
    public class Vote
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public UserEntity? User { get; set; }

        public Guid CoinId { get; set; }
        public Coin? Coin { get; set; }

        public Guid DailyVotingSessionId { get; set; }
        public DailyVotingSession? DailyVotingSession { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}