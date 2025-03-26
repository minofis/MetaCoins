using MetaCoins.Core.Entities.Voting;

namespace MetaCoins.Core.Interfaces.Repositories
{
    public interface IVotesRepository
    {
        Task VoteCoinAsync(Vote vote);
        Task UnvoteCoinAsync(Guid userId, Guid coinId);
        Task<bool> IsCoinVotedAsync(Guid userId, Guid coinId);
    }
}