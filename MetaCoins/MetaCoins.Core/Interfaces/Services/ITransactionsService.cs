using MetaCoins.Core.Entities;

namespace MetaCoins.Core.Interfaces.Services
{
    public interface ITransactionsService
    {
        Task<List<Transaction>> GetAllTransactionsAsync();
        Task<Transaction> GetTransactionByIdAsync(Guid transactionId);
        Task TransferCoinAsync(Guid senderWalletId, Guid recipientWalletId, Guid coinId);
        Task CreateTransactionAsync(Transaction transaction);
    }
}