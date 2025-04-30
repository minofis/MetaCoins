using MetaCoins.Core.Entities.Votes;

namespace MetaCoins.Core.Interfaces.Repositories
{
    public interface IVotingSessionsRepository
    {
        Task<List<VotingSession>> GetActiveVotingSessionsAsync();
        Task<VotingSession> GetVotingSessionByIdAsync(Guid votingSessionId);
        Task CreateVotingSessionAsync(VotingSession votingSession);
        Task<bool> VotingSessionExistsAsync(Guid votingSessionId);
        Task DeactivateExpiredVotingSessionsAsync();
    }
}