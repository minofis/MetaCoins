using MetaCoins.Core.Entities.Votes;

namespace MetaCoins.Core.Interfaces.Repositories
{
    public interface ICoinVotesRepository
    {
        Task VoteCoinAsync(CoinVote coinVote);
        Task UnvoteCoinAsync(Guid votingSessionId, Guid userId, Guid coinId);
        Task<bool> IsCoinVotedAsync(Guid votingSessionId, Guid userId, Guid coinId);
        Task<bool> HasUserVotedInVotingSessionAsync(Guid votingSessionId, Guid userId);
    }
}