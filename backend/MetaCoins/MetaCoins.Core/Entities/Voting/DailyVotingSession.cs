namespace MetaCoins.Core.Entities.Voting
{
    public class DailyVotingSession
    {
        public Guid Id { get; set; }
        public bool IsActive { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public Guid WeeklyVotingSessionId { get; set; }
        public WeeklyVotingSession WeeklyVotingSession { get; set; }

        public Guid DailyVoteId { get; set; }
        public DailyVote DailyVote { get; set; }
        
        public List<Vote> Votes { get; set; }

        public Guid? WinnerId { get; set; }
        public Coin? Winner { get; set; }
    }
}