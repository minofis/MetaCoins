using MetaCoins.Core.Entities;
using MetaCoins.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MetaCoins.DAL.Data.Repositories
{
    public class TransactionsRepository : ITransactionsRepository
    {
        private readonly MetaCoinsDbContext _context;
        public TransactionsRepository(MetaCoinsDbContext context)
        {
            _context = context;
        }

        public async Task<List<Transaction>> GetAllTransactionsAsync()
        {
            return await _context.Transactions
                .Include(t => t.Status)
                .Include(t => t.Type)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Transaction> GetTransactionByIdAsync(Guid transactionId)
        {
            return await _context.Transactions
                .Include(t => t.Status)
                .Include(t => t.Type)
                .FirstOrDefaultAsync(t => t.Id == transactionId);
        }

        public async Task CreateTransactionAsync(Transaction transaction)
        {
            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetTransactionTypeId(string name)
        {
            var transactionType = await _context.TransactionTypes.FirstOrDefaultAsync(t => t.Name == name);

            return transactionType.Id;
        }

        public async Task<int> GetTransactionStatusId(string name)
        {
            var transactionStatus = await _context.TransactionStatuses.FirstOrDefaultAsync(s => s.Name == name);

            return transactionStatus.Id;
        }
    }
}