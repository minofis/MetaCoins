using MetaCoins.Core.Entities;

namespace MetaCoins.Core.Interfaces.Repositories
{
    public interface ITransactionsRepository
    {
        Task<List<Transaction>> GetAllTransactionsAsync();
        Task<Transaction> GetTransactionByIdAsync(Guid transactionId);
        Task CreateTransactionAsync(Transaction transaction);
        Task<int> GetTransactionTypeId(string name);
        Task<int> GetTransactionStatusId(string name);
    }
}