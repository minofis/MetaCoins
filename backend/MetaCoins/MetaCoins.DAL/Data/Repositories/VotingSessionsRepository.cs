using MetaCoins.Core.Entities.Votes;
using MetaCoins.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MetaCoins.DAL.Data.Repositories
{
    public class VotingSessionsRepository : IVotingSessionsRepository
    {
        private readonly MetaCoinsDbContext _context;
        public VotingSessionsRepository(MetaCoinsDbContext context)
        {
            _context = context;
        }
        public async Task<List<VotingSession>> GetActiveVotingSessionsAsync()
        {
            return await _context.VotingSessions
                .Include(vs => vs.SubVotingSessions)
                .Include(vs => vs.CoinVotingSessions)
                .Include(vs => vs.VotingType)
                .Where(vs => vs.IsActive == true)
                .ToListAsync();
        }

        public async Task<VotingSession> GetVotingSessionByIdAsync(Guid votingSessionId)
        {
            return await _context.VotingSessions
                .Include(vs => vs.SubVotingSessions)
                .Include(vs => vs.CoinVotingSessions)
                .Include(vs => vs.VotingType)
                .Include(vs => vs.CoinVotes)
                .FirstOrDefaultAsync(vs => vs.Id == votingSessionId);
        }

        public async Task CreateVotingSessionAsync(VotingSession votingSession)
        {
            await _context.VotingSessions.AddAsync(votingSession);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> VotingSessionExistsAsync(Guid votingSessionId)
        {
            return await _context.VotingSessions
                .AnyAsync(vs => vs.Id == votingSessionId);
        }

        public async Task UpdateVotingSessionStatusAsync(Guid votingSessionId, bool status)
        {
            await _context.VotingSessions
                .Where(vs => vs.Id == votingSessionId)
                .ExecuteUpdateAsync(s => s.SetProperty(vs => vs.IsActive, status));
        }

        public async Task UpdateVotingSessionWinnerAsync(Guid votingSessionId, Guid winnerId)
        {
            await _context.VotingSessions
                .Where(vs => vs.Id == votingSessionId)
                .ExecuteUpdateAsync(s => s.SetProperty(dvs => dvs.WinnerId, winnerId));
        }

        public async Task AddCoinsToVotingSessionAsync(IEnumerable<CoinVotingSession> cvs)
        {
            await _context.CoinVotingSessions.AddRangeAsync(cvs);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateVotingSessionAsync(VotingSession votingSession)
        {
            _context.VotingSessions.Update(votingSession);
            await _context.SaveChangesAsync();
        }
    }
}