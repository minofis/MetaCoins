using MetaCoins.Core.Entities.Voting;

namespace MetaCoins.Core.Interfaces.Repositories
{
    public interface IVotesRepository
    {
        Task<DailyVotingSession> GetActiveDailySessionAsync();
        Task<WeeklyVotingSession> GetActiveWeeklySessionAsync();

        Task<bool> DailySessionExistsAsync(Guid dailySessionId);
        Task<bool> HasUserVotedInDailySessionAsync(Guid dailySessionId, Guid userId);

        Task<DailyVote> GetDailyVoteByIdAsync(Guid dailyVoteId);
        Task<WeeklyVotingSession> GetWeeklySessionByIdAsync(Guid weeklySessionId);
        Task<DailyVotingSession> GetDailySessionByIdAsync(Guid dailySessionId);

        Task VoteCoinAsync(Vote vote);
        Task UnvoteCoinAsync(Guid dailySessionId, Guid userId, Guid coinId);
        Task<bool> IsCoinVotedAsync(Guid dailySessionId, Guid userId, Guid coinId);

        Task CreateWeeklySessionAsync(WeeklyVotingSession weeklySession, List<DailyVote> dailyVotes, List<DailyVotingSession> dailySessions);

        Task DeactivateExpiredDailySessionsAsync();
        Task DeactivateExpiredWeeklySessionsAsync();
        Task ActivateStartedDailySessionsAsync();
        
        Task SaveChangesAsync();
    }
}