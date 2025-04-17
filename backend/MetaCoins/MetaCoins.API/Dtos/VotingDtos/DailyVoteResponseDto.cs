using MetaCoins.API.Dtos.CoinDtos;

namespace MetaCoins.API.Dtos.VotingDtos
{
    public class DailyVoteResponseDto
    {
        public Guid Id { get; set; }
        public Guid DailyVotingSessionId { get; set; }
        public Guid[] CoinIds { get; set; }
    }
}