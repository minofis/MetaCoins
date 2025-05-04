using MetaCoins.Core.Entities;
using MetaCoins.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MetaCoins.DAL.Data.Repositories
{
    public class CoinSellOrdersRepository : ICoinSellOrdersRepository
    {
        private readonly MetaCoinsDbContext _context;
        public CoinSellOrdersRepository(MetaCoinsDbContext context)
        {
            _context = context;
        }
        public async Task CreateCoinSellOrderAsync(CoinSellOrder coinSellOrder)
        {
            await _context.CoinSellOrders.AddAsync(coinSellOrder);
            await _context.SaveChangesAsync();
        }

        public async Task<CoinSellOrder> GetCoinSellOrderById(Guid coinSellOrderId)
        {
            return await _context.CoinSellOrders
                .Include(cso => cso.Status)
                .FirstOrDefaultAsync(cso => cso.Id == coinSellOrderId);
        }

        public async Task UpdateCoinSellOrderAsync(CoinSellOrder coinSellOrder)
        {
            _context.CoinSellOrders.Update(coinSellOrder);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCoinSellOrderStatusAsync(Guid coinSellOrderId, int statusId)
        {
            await _context.CoinSellOrders
                .Where(cso => cso.Id == coinSellOrderId)
                .ExecuteUpdateAsync(s => s.SetProperty(cso => cso.StatusId, statusId));
        }
    }
}