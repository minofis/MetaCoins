using MetaCoins.Core.Entities;
using MetaCoins.Core.Entities.Votes;

namespace MetaCoins.Core.Interfaces.Services
{
    public interface IVotingSessionsService
    {
        Task<List<VotingSession>> GetActiveVotingSessionsAsync();
        Task<VotingSession> GetVotingSessionByIdAsync(Guid votingSessionId);
        Task CreateVotingSessionAsync(string title, int votingTypeId, DateTime endDate, List<Guid> coinIds, Guid? parentSessionId);
        Task AddCoinsToVotingSessionAsync(Guid votingSessionId, IEnumerable<Guid> coinIds);
        Task<bool> VotingSessionExistsAsync(Guid votingSessionId);
        Task DetermineWinnerAsync(Guid votingSessionId);
        Task DeactivateVotingSessionAsync(Guid votingSessionId);
        Task ScheduleVotingSessionEndJob(VotingSession votingSession);
    }
}