namespace MetaCoins.API.Dtos.VotingDtos
{
    public class CoinVoteResponseDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid CoinId { get; set; }
        public Guid VotingSessionId { get; set; }
        public string CreatedAt { get; set; } = string.Empty;
    }
}