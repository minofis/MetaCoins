using MetaCoins.Core.Entities;
using MetaCoins.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MetaCoins.DAL.Data.Repositories
{
    public class CoinTransactionsRepository : ICoinTransactionsRepository
    {
        private readonly MetaCoinsDbContext _context;
        public CoinTransactionsRepository(MetaCoinsDbContext context)
        {
            _context = context;
        }
        public async Task CreateCoinTransactionAsync(CoinTransaction coinTransaction)
        {
            await _context.CoinTransactions.AddAsync(coinTransaction);
            await _context.SaveChangesAsync();
        }

        public async Task<CoinTransaction> GetCoinTransactionById(Guid coinTransactionId)
        {
            return await _context.CoinTransactions
                .Include(ct => ct.Status)
                .Include(ct => ct.Type)
                .FirstOrDefaultAsync(ct => ct.Id == coinTransactionId);
        }

        public async Task UpdateCoinTransactionAsync(CoinTransaction coinTransaction)
        {
            _context.CoinTransactions.Update(coinTransaction);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCoinTransactionStatusAsync(Guid coinTransactionId, int statusId)
        {
            await _context.CoinTransactions
                .Where(ct => ct.Id == coinTransactionId)
                .ExecuteUpdateAsync(s => s.SetProperty(ct => ct.StatusId, statusId));
        }
    }
}