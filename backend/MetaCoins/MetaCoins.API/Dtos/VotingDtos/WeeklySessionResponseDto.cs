namespace MetaCoins.API.Dtos.VotingDtos
{
    public class WeeklySessionResponseDto
    {
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
        public string StartDate { get; set; } = string.Empty;
        public string EndDate { get; set; } = string.Empty;
        public Guid[] DailySessionIds { get; set; } = [];
        public Guid? WinnerId { get; set; }
    }
}