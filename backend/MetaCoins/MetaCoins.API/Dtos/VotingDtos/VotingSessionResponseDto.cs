namespace MetaCoins.API.Dtos.VotingDtos
{
    public class VotingSessionResponseDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string VotingType { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public string StartDate { get; set; } = string.Empty;
        public string EndDate { get; set; } = string.Empty;
        public Guid? ParentVotingSessionId { get; set; }
        public Guid[] SubVotingSessionIds { get; set; } = [];
        public Guid[] VoteCoinIds { get; set; } = [];
        public Guid? WinnerId { get; set; }
    }
}