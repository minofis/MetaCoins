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

        public async Task<DailyVotingSession> GetDailySessionByDateAsync(DateTime dailySessionDate)
        {
            return await _context.DailyVotingSessions
                .Include(dvs => dvs.DailyVote)
                    .ThenInclude(dv => dv.Coins)
                .FirstOrDefaultAsync(dvs => dvs.StartDate.Date == dailySessionDate.Date);
        }

        public async Task VoteCoinAsync(Vote vote)
        {
            await _context.Votes.AddAsync(vote);
            await _context.SaveChangesAsync();
        }

        public async Task UnvoteCoinAsync(Guid userId, Guid coinId)
        {
            var vote = _context.Votes.FirstOrDefault(v => v.CoinId == coinId && v.UserId == userId);
            _context.Votes.Remove(vote);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsCoinVotedAsync(Guid userId, Guid coinId)
        {
            var isVoted = await _context.Votes.AnyAsync(v => v.CoinId == coinId && v.UserId == userId);

            return isVoted;
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
                .Where(dvs => DateTime.Compare(dvs.EndDate, DateTime.Now.ToUniversalTime()) <= 0)
                .ExecuteUpdateAsync(s => s.SetProperty(dvs => dvs.IsActive, false));
        }

        public async Task DeactivateExpiredWeeklySessionsAsync()
        {
            await _context.WeeklyVotingSessions
                .Where(wvs => DateTime.Compare(wvs.EndDate, DateTime.Now.ToUniversalTime()) <= 0)
                .ExecuteUpdateAsync(s => s.SetProperty(wvs => wvs.IsActive, false));
        }

        public async Task ActivateStartedDailySessionsAsync()
        {
            await _context.DailyVotingSessions
                .Where(dvs => DateTime.Compare(dvs.StartDate, DateTime.Now.ToUniversalTime()) == 0)
                .ExecuteUpdateAsync(s => s.SetProperty(dvs => dvs.IsActive, true));
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}