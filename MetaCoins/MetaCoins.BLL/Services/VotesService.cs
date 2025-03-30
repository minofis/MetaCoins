using MetaCoins.Core.Entities.Helpers;
using MetaCoins.Core.Entities.Voting;
using MetaCoins.Core.Interfaces.Repositories;
using MetaCoins.Core.Interfaces.Services;

namespace MetaCoins.BLL.Services
{
    public class VotesService : IVotesService
    {
        private readonly IVotesRepository _votesRepo;
        private readonly ICoinsService _coinsService;
        private readonly IUsersService _usersService;
        public VotesService(IVotesRepository votesRepo, IUsersService usersService, ICoinsService coinsService)
        {
            _votesRepo = votesRepo;
            _usersService = usersService;
            _coinsService = coinsService;
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

        public async Task UnvoteCoinAsync(Guid userId, Guid coinId)
        {
            var user = await _usersService.GetUserByIdAsync(userId);

            if(!user.Votes.Any(l => l.CoinId == coinId))
            {
                throw new ArgumentException($"Coin with ID {coinId} is not voted.");
            }

            await _votesRepo.UnvoteCoinAsync(userId, coinId);
        }

        public async Task<bool> IsCoinVotedAsync(Guid userId, Guid coinId)
        {
            var isVoted = await _votesRepo.IsCoinVotedAsync(userId, coinId);
            
            return isVoted;
        }

        public async Task CreateWeeklySessionAsync()
        {
            var query = new CoinQueryObject
            {
                SortBy = new List<(string Field, bool Descending)>
                {
                    ("likes", true),
                    ("createdAt", true)
                },
                PageNumber = 1,
                PageSize = 8
            };

            var paginatedCoins = await _coinsService.GetAllCoinsAsync(query);

            if (paginatedCoins.TotalItems < 8)
            {
                throw new ArgumentException("Not enough coins created for weekly voting");
            }

            var today = DateTime.Now.ToUniversalTime();
            //if (await _context.DailyVotingSessions.AnyAsync(dvs => dvs.StartDate == today)) return;

            var weeklySession = new WeeklyVotingSession
            {
                Id = Guid.NewGuid(),
                IsActive = true,
                StartDate = today,
                EndDate = today.AddDays(7)
            };

            List<DailyVotingSession> dailySessions = new();
            List<DailyVote> dailyVotes = new();

            for (int daysCount = 0; daysCount < 7; daysCount++)
            {
                var dailySession = new DailyVotingSession
                {
                    Id = Guid.NewGuid(),
                    IsActive = false,
                    StartDate = today.Date.AddDays(daysCount),
                    EndDate = today.Date.AddDays(daysCount).Add(new TimeSpan(23,59,59)),
                    WeeklyVotingSessionId = weeklySession.Id
                };

                var dailyVote = new DailyVote
                {
                    Id = Guid.NewGuid(),
                    DailyVotingSessionId = dailySession.Id,
                };

                if (daysCount < 4)
                {
                    dailyVote.Coins = paginatedCoins.Items.Skip(daysCount * 2).Take(2).ToList();
                }

                dailySessions.Add(dailySession);
                dailyVotes.Add(dailyVote);
            }

            await _votesRepo.CreateWeeklySessionAsync(weeklySession, dailyVotes, dailySessions);
        }

        public async Task CalculateDailySessionResultAsync(DateTime dailySessionDate)
        {
            var dailySession = await _votesRepo.GetDailySessionByDateAsync(dailySessionDate);

            var coins = dailySession.DailyVote.Coins;

            var winner = coins.MaxBy(c => c.VotesCount)
                ?? throw new ArgumentException("There is no winner in the daily voting session");

            dailySession.WinnerId = winner.Id;

            await _votesRepo.SaveChangesAsync();
        }

        public async Task DeactivateExpiredDailySessionsAsync()
        {
            await _votesRepo.DeactivateExpiredDailySessionsAsync();
        }

        public async Task DeactivateExpiredWeeklySessionsAsync()
        {
            await _votesRepo.DeactivateExpiredWeeklySessionsAsync();
        }

        public async Task ActivateStartedDailySessionsAsync()
        {
            await _votesRepo.ActivateStartedDailySessionsAsync();
        }
    }
}