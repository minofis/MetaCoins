namespace MetaCoins.Core.Entities.Voting
{
    public class WeeklyVotingSession
    {
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
        
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public List<DailyVotingSession> DailySessions { get; set; }

        public Guid? WinnerId { get; set; }
        public Coin? Winner { get; set; }
    }
}