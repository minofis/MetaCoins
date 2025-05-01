using MetaCoins.Core.Entities.Votes;
using MetaCoins.Core.Interfaces.Repositories;
using MetaCoins.Core.Interfaces.Services;

namespace MetaCoins.BLL.Services
{
    public class CoinVotesService : ICoinVotesService
    {
        private readonly ICoinVotesRepository _coinVotesRepo;
        private readonly IUsersService _usersService;
        private readonly IVotingSessionsService _votingSessionsService;
        public CoinVotesService(ICoinVotesRepository coinVotesRepo, IUsersService usersService, IVotingSessionsService votingSessionsService)
        {
            _coinVotesRepo = coinVotesRepo;
            _usersService = usersService;
            _votingSessionsService = votingSessionsService;
        }

        public async Task<List<CoinVote>> GetCoinVotesByVotingSessionIdAsync(Guid votingSessionId)
        {
            var sessionExists = await _votingSessionsService.VotingSessionExistsAsync(votingSessionId);
            if(!sessionExists)
                throw new ArgumentException($"Voting session with ID {votingSessionId} does not exist.");

            return await _coinVotesRepo.GetCoinVotesByVotingSessionIdAsync(votingSessionId);
        }

        public async Task VoteCoinAsync(Guid votingSessionId, Guid userId, Guid coinId)
        {
            var userExists = await _usersService.UserExistsAsync(userId);
            if(!userExists)
                throw new ArgumentException($"User with ID {userId} does not exist.");

            var sessionExists = await _votingSessionsService.VotingSessionExistsAsync(votingSessionId);
            if(!sessionExists)
                throw new ArgumentException($"Voting session with ID {votingSessionId} does not exist.");

            var hasVotedInSession = await _coinVotesRepo.HasUserVotedInVotingSessionAsync(votingSessionId, userId);
            if (hasVotedInSession)
                throw new ArgumentException($"This user already voted in voting session with ID {votingSessionId}.");

            var coinVote = new CoinVote
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                CoinId = coinId,
                VotingSessionId = votingSessionId,
                CreatedAt = DateTime.UtcNow
            };

            await _coinVotesRepo.VoteCoinAsync(coinVote);
        }

        public async Task UnvoteCoinAsync(Guid votingSessionId, Guid userId, Guid coinId)
        {
            var isVoted = await _coinVotesRepo.IsCoinVotedAsync(votingSessionId, userId, coinId);
            if (!isVoted)
                throw new ArgumentException($"Coin with ID {coinId} is not voted.");

            await _coinVotesRepo.UnvoteCoinAsync(votingSessionId, userId, coinId);
        }

        public async Task<bool> IsCoinVotedAsync(Guid votingSessionId, Guid userId, Guid coinId)
        {
            return await _coinVotesRepo.IsCoinVotedAsync(votingSessionId, userId, coinId);
        }

        public async Task<bool> HasUserVotedInVotingSessionAsync(Guid votingSessionId, Guid userId)
        {
            return await _coinVotesRepo.HasUserVotedInVotingSessionAsync(votingSessionId, userId);
        }
    }
}