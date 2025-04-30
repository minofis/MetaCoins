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
                .Where(vs => vs.IsActive == true)
                .ToListAsync();
        }

        public async Task<VotingSession> GetVotingSessionByIdAsync(Guid votingSessionId)
        {
            return await _context.VotingSessions
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

        public async Task DeactivateExpiredVotingSessionsAsync()
        {
            await _context.VotingSessions
                .Where(vs => DateTime.Compare(vs.EndDate, DateTime.UtcNow) <= 0)
                .ExecuteUpdateAsync(s => s.SetProperty(dvs => dvs.IsActive, false));
        }
    }
}