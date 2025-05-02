namespace MetaCoins.API.Dtos.VotingDtos
{
    public class VotingSessionRequestDto
    {
        public string Title { get; set; } = string.Empty;
        public int VotingTypeId { get; set; }
        public DateTime EndDate { get; set; }
        public List<Guid> CoinIds { get; set; } = new();
        public Guid? ParentVotingSessionId { get; set; }
    }
}