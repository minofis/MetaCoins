using MetaCoins.Core.Entities.Votes;

namespace MetaCoins.Core.Interfaces.Services
{
    public interface IVotingSessionsService
    {
        Task<List<VotingSession>> GetActiveVotingSessionsAsync();
        Task<VotingSession> GetVotingSessionByIdAsync(Guid votingSessionId);
        Task CreateVotingSessionAsync();
        Task<bool> VotingSessionExistsAsync(Guid votingSessionId);
        Task DetermineWinnerAsync();
        Task DeactivateExpiredVotingSessionsAsync();
    }
}