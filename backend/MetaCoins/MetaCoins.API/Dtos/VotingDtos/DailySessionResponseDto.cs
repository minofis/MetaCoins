namespace MetaCoins.API.Dtos.VotingDtos
{
    public class DailySessionResponseDto
    {
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public Guid DailyVoteId { get; set; }
        public Guid WeeklyVotingSessionId { get; set; }
        public Guid? WinnerId { get; set; }
    }
}