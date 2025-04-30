namespace MetaCoins.Core.Interfaces.Services
{
    public interface ICoinVotesService
    {
        Task VoteCoinAsync(Guid votingSessionId, Guid userId, Guid coinId);
        Task UnvoteCoinAsync(Guid votingSessionId, Guid userId, Guid coinId);
        Task<bool> IsCoinVotedAsync(Guid votingSessionId, Guid userId, Guid coinId);
        Task<bool> HasUserVotedInVotingSessionAsync(Guid votingSessionId, Guid userId);
    }
}