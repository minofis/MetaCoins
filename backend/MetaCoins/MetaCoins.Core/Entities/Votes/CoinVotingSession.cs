namespace MetaCoins.Core.Entities.Votes
{
    public class CoinVotingSession
    {
        public Guid CoinId { get; set; }
        public Coin Coin { get; set; } = null!;
        public Guid VotingSessionId { get; set; }
        public VotingSession VotingSession { get; set; } = null!;
    }
}