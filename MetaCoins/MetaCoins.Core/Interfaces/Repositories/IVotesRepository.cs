using MetaCoins.Core.Entities.Voting;

namespace MetaCoins.Core.Interfaces.Repositories
{
    public interface IVotesRepository
    {
        Task<DailyVotingSession> GetDailySessionByDateAsync(DateTime dailySessionDate);
        Task VoteCoinAsync(Vote vote);
        Task UnvoteCoinAsync(Guid userId, Guid coinId);
        Task<bool> IsCoinVotedAsync(Guid userId, Guid coinId);
        Task CreateWeeklySessionAsync(WeeklyVotingSession weeklySession, List<DailyVote> dailyVotes, List<DailyVotingSession> dailySessions);
        Task DeactivateExpiredDailySessionsAsync();
        Task DeactivateExpiredWeeklySessionsAsync();
        Task ActivateStartedDailySessionsAsync();
        Task SaveChangesAsync();
    }
}