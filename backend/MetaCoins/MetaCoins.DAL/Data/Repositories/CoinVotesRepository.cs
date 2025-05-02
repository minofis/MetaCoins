using MetaCoins.Core.Entities.Votes;
using MetaCoins.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MetaCoins.DAL.Data.Repositories
{
    public class CoinVotesRepository : ICoinVotesRepository
    {
        private readonly MetaCoinsDbContext _context;
        public CoinVotesRepository(MetaCoinsDbContext context)
        {
            _context = context;
        }
        public async Task<List<CoinVote>> GetCoinVotesByVotingSessionIdAsync(Guid votingSessionId)
        {
            return await _context.CoinVotes
                .Where(cv => cv.VotingSessionId == votingSessionId)
                .ToListAsync();
        }

        public async Task VoteCoinAsync(CoinVote coinVote)
        {
            await _context.CoinVotes.AddAsync(coinVote);
            await _context.SaveChangesAsync();
        }
        
        public async Task UnvoteCoinAsync(Guid votingSessionId, Guid userId, Guid coinId)
        {
            await _context.CoinVotes
                .Where(
                    cv => cv.CoinId == coinId 
                    && cv.UserId == userId
                    && cv.VotingSessionId == votingSessionId)
                .ExecuteDeleteAsync();
        }

        public async Task<bool> IsCoinVotedAsync(Guid votingSessionId, Guid userId, Guid coinId)
        {
            return await _context.CoinVotes
                .AnyAsync(
                    cv => cv.CoinId == coinId 
                    && cv.UserId == userId
                    && cv.VotingSessionId == votingSessionId);
        }

        public async Task<bool> HasUserVotedInVotingSessionAsync(Guid votingSessionId, Guid userId)
        {
            return await _context.CoinVotes
                .AnyAsync(
                    cv => cv.UserId == userId
                    && cv.VotingSessionId == votingSessionId);
        }
    }
}