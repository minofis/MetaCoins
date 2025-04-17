using MetaCoins.Core.Entities.Voting;
using MetaCoins.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MetaCoins.DAL.Data.Repositories
{
    public class VotesRepository : IVotesRepository
    {
        private readonly MetaCoinsDbContext _context;
        public VotesRepository(MetaCoinsDbContext context)
        {
            _context = context;
        }

        public async Task<DailyVote> GetDailyVoteByIdAsync(Guid dailyVoteId)
        {
            return await _context.DailyVotes
                .Include(dv => dv.Coins)
                .FirstOrDefaultAsync(dv => dv.Id == dailyVoteId);
        }

        public async Task<WeeklyVotingSession> GetWeeklySessionByIdAsync(Guid weeklySessionId)
        {
            return await _context.WeeklyVotingSessions
                .Include(wvs => wvs.DailySessions)
                .FirstOrDefaultAsync(wvs => wvs.Id == weeklySessionId);
        }

        public async Task<DailyVotingSession> GetDailySessionByIdAsync(Guid dailySessionId)
        {
            return await _context.DailyVotingSessions
                .Include(dvs => dvs.DailyVote)
                    .ThenInclude(dv => dv.Coins)
                .FirstOrDefaultAsync(dvs => dvs.Id == dailySessionId);
        }

        public async Task<bool> DailySessionExistsAsync(Guid dailySessionId)
        {
            return await _context.DailyVotingSessions
                .AnyAsync(dvs => dvs.Id == dailySessionId);
        }

        public async Task VoteCoinAsync(Vote vote)
        {
            await _context.Votes.AddAsync(vote);
            await _context.SaveChangesAsync();
        }

        public async Task UnvoteCoinAsync(Guid dailySessionId, Guid userId, Guid coinId)
        {
            await _context.Votes
                .Where(
                v => v.CoinId == coinId 
                && v.UserId == userId
                && v.DailyVotingSessionId == dailySessionId)
                .ExecuteDeleteAsync();
        }

        public async Task<bool> IsCoinVotedAsync(Guid dailySessionId, Guid userId, Guid coinId)
        {
            return await _context.Votes
                .AnyAsync(
                    v => v.CoinId == coinId 
                    && v.UserId == userId
                    && v.DailyVotingSessionId == dailySessionId);
        }
        
        public async Task<bool> HasUserVotedInDailySessionAsync(Guid dailySessionId, Guid userId)
        {
            return await _context.Votes
                .AnyAsync(
                    v => v.UserId == userId
                         && v.DailyVotingSessionId == dailySessionId);
        }

        public async Task CreateWeeklySessionAsync(WeeklyVotingSession weeklySession, List<DailyVote> dailyVotes, List<DailyVotingSession> dailySessions)
        {
            await _context.WeeklyVotingSessions.AddAsync(weeklySession);
            await _context.DailyVotes.AddRangeAsync(dailyVotes);
            await _context.DailyVotingSessions.AddRangeAsync(dailySessions);
            await _context.SaveChangesAsync();
        }

        public async Task DeactivateExpiredDailySessionsAsync()
        {
            await _context.DailyVotingSessions
                .Where(dvs => DateTime.Compare(dvs.EndDate, DateTime.UtcNow) <= 0)
                .ExecuteUpdateAsync(s => s.SetProperty(dvs => dvs.IsActive, false));
        }

        public async Task DeactivateExpiredWeeklySessionsAsync()
        {
            await _context.WeeklyVotingSessions
                .Where(wvs => DateTime.Compare(wvs.EndDate, DateTime.UtcNow) <= 0)
                .ExecuteUpdateAsync(s => s.SetProperty(wvs => wvs.IsActive, false));
        }

        public async Task ActivateStartedDailySessionsAsync()
        {
            await _context.DailyVotingSessions
                .Where(dvs => DateTime.Compare(dvs.StartDate, DateTime.UtcNow) == 0)
                .ExecuteUpdateAsync(s => s.SetProperty(dvs => dvs.IsActive, true));
        }

        public async Task<DailyVotingSession> GetActiveDailySessionAsync()
        {
            return await _context.DailyVotingSessions
                .Include(dvs => dvs.DailyVote)
                    .ThenInclude(dv => dv.Coins)
                .FirstOrDefaultAsync(dvs => dvs.IsActive == true);
        }

        public async Task<WeeklyVotingSession> GetActiveWeeklySessionAsync()
        {
            return await _context.WeeklyVotingSessions
                .Include(wvs => wvs.DailySessions)
                .FirstOrDefaultAsync(wvs => wvs.IsActive == true);
        }
        
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}