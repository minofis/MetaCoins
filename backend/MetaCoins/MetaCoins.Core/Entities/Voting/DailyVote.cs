namespace MetaCoins.Core.Entities.Voting
{
    public class DailyVote
    {
        public Guid Id { get; set; }

        public Guid DailyVotingSessionId { get; set; }
        public DailyVotingSession DailyVotingSession { get; set; }

        public List<Coin> Coins { get; set; }
    }
}