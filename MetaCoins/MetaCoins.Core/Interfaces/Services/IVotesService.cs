namespace MetaCoins.Core.Interfaces.Services
{
    public interface IVotesService
    {
        Task VoteCoinAsync(Guid userId, Guid coinId);
        Task UnvoteCoinAsync(Guid userId, Guid coinId);
        Task<bool> IsCoinVotedAsync(Guid userId, Guid coinId);
    }
}