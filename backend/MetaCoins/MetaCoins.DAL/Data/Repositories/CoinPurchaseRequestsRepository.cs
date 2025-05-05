using MetaCoins.Core.Entities;
using MetaCoins.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MetaCoins.DAL.Data.Repositories
{
    public class CoinPurchaseRequestsRepository : ICoinPurchaseRequestsRepository
    {
        private readonly MetaCoinsDbContext _context;
        public CoinPurchaseRequestsRepository(MetaCoinsDbContext context)
        {
            _context = context;
        }

        public async Task<CoinPurchaseRequest> GetCoinPurchaseRequestById(Guid coinPurchaseRequestId)
        {
            return await _context.CoinPurchaseRequests
                .Include(cpr => cpr.Status)
                .FirstOrDefaultAsync(cpr => cpr.Id == coinPurchaseRequestId);
        }

        public async Task CreateCoinPurchaseRequestAsync(CoinPurchaseRequest coinPurchaseRequest)
        {
            await _context.CoinPurchaseRequests.AddAsync(coinPurchaseRequest);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCoinPurchaseRequestAsync(Guid coinPurchaseRequestId)
        {
            await _context.CoinPurchaseRequests
                .Where(cpr => cpr.Id == coinPurchaseRequestId)
                .ExecuteDeleteAsync();
        }

        public async Task UpdateCoinPurchaseRequestStatusAsync(Guid coinPurchaseRequestId, int statusId)
        {
            await _context.CoinPurchaseRequests
                .Where(cpr => cpr.Id == coinPurchaseRequestId)
                .ExecuteUpdateAsync(s => s.SetProperty(cpr => cpr.StatusId, statusId));
        }
    }
}