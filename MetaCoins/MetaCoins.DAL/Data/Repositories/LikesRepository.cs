using MetaCoins.Core.Entities;
using MetaCoins.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MetaCoins.DAL.Data.Repositories
{
    public class LikesRepository : ILikesRepository
    {
        private readonly MetaCoinsDbContext _context;
        public LikesRepository(MetaCoinsDbContext context)
        {
            _context = context;
        }

        public async Task LikeCoinAsync(Like like)
        {
            await _context.Likes.AddAsync(like);
            await _context.SaveChangesAsync();
        }

        public async Task UnlikeCoinAsync(Guid userId, Guid coinId)
        {
            var like = _context.Likes.FirstOrDefault(l => l.CoinId == coinId && l.UserId == userId);
            _context.Likes.Remove(like);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsCoinLikedAsync(Guid userId, Guid coinId)
        {
            var isLiked = await _context.Likes.AnyAsync(l => l.CoinId == coinId && l.UserId == userId);

            return isLiked;
        }

        public async Task<List<Coin>> GetLikedCoinsByUsernameAsync(string username)
        {
            return await _context.Likes
                .Include(lc => lc.User)
                .Include(lc => lc.Coin)
                    .ThenInclude(c => c.Likes)
                .Include(lc => lc.Coin)
                    .ThenInclude(c => c.Wallet)
                        .ThenInclude(w => w.User)
                .Where(lc => lc.User.UserName == username)
                .Select(lc => lc.Coin)
                .ToListAsync();
        }
    }
}