using MetaCoins.Core.Entities.Votes;

namespace MetaCoins.Core.Interfaces.Repositories
{
    public interface IVotingSessionsRepository
    {
        Task<List<VotingSession>> GetActiveVotingSessionsAsync();
        Task<VotingSession> GetVotingSessionByIdAsync(Guid votingSessionId);
        Task CreateVotingSessionAsync(VotingSession votingSession);
        Task<bool> VotingSessionExistsAsync(Guid votingSessionId);
        Task UpdateVotingSessionAsync(VotingSession votingSession);
        Task UpdateVotingSessionStatusAsync(Guid votingSessionId, bool status);
        Task UpdateVotingSessionWinnerAsync(Guid votingSessionId, Guid winnerId);
        Task AddCoinsToVotingSessionAsync(IEnumerable<CoinVotingSession> cvs);
    }
}