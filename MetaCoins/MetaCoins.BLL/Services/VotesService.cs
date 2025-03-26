using MetaCoins.Core.Entities.Voting;
using MetaCoins.Core.Interfaces.Repositories;
using MetaCoins.Core.Interfaces.Services;

namespace MetaCoins.BLL.Services
{
    public class VotesService : IVotesService
    {
        private readonly IVotesRepository _votesRepo;
        private readonly IUsersService _usersService;
        public VotesService(IVotesRepository votesRepo, IUsersService usersService)
        {
            _votesRepo = votesRepo;
            _usersService = usersService;
        }

        public async Task VoteCoinAsync(Guid userId, Guid coinId)
        {
            var user = await _usersService.GetUserByIdAsync(userId);

            if(user.Votes.Any(v => v.CoinId == coinId))
            {
                throw new ArgumentException($"Coin with ID {coinId} already voted.");
            }

            var vote = new Vote
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                CoinId = coinId,
                CreatedAt = DateTime.Now.ToUniversalTime()
            };

            await _votesRepo.VoteCoinAsync(vote);
        }

        public async Task<bool> IsCoinVotedAsync(Guid userId, Guid coinId)
        {
            var isVoted = await _votesRepo.IsCoinVotedAsync(userId, coinId);
            
            return isVoted;
        }

        public async Task UnvoteCoinAsync(Guid userId, Guid coinId)
        {
            var user = await _usersService.GetUserByIdAsync(userId);

            if(!user.Votes.Any(l => l.CoinId == coinId))
            {
                throw new ArgumentException($"Coin with ID {coinId} is not voted.");
            }

            await _votesRepo.UnvoteCoinAsync(userId, coinId);
        }
    }
}