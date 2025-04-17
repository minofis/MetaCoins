using MetaCoins.Core.Entities.Voting;

namespace MetaCoins.Core.Interfaces.Services
{
    public interface IVotesService
    {
        Task<DailyVotingSession> GetActiveDailySessionAsync();
        Task<WeeklyVotingSession> GetActiveWeeklySessionAsync();

        Task<DailyVote> GetDailyVoteByIdAsync(Guid dailyVoteId);
        Task<WeeklyVotingSession> GetWeeklySessionByIdAsync(Guid weeklySessionId);
        Task<DailyVotingSession> GetDailySessionByIdAsync(Guid dailySessionId);

        Task VoteCoinAsync(Guid dailySessionId, Guid userId, Guid coinId);
        Task UnvoteCoinAsync(Guid dailySessionId, Guid userId, Guid coinId);
        Task<bool> IsCoinVotedAsync(Guid dailySessionId, Guid userId, Guid coinId);

        Task CreateWeeklySessionAsync();
        
        Task DeactivateExpiredDailySessionsAsync();
        Task DeactivateExpiredWeeklySessionsAsync();
        Task ActivateStartedDailySessionsAsync();
        Task CalculateDailySessionResultAsync();
    }
}