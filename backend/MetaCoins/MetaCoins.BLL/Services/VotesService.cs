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

        public async Task<DailyVotingSession> GetActiveDailySessionAsync()
        {
            // Get active daily session
            var dailySession = await _votesRepo.GetActiveDailySessionAsync()
                ?? throw new ArgumentException($"There is no active daily voting session.");

            return dailySession;
        }

        public async Task<WeeklyVotingSession> GetActiveWeeklySessionAsync()
        {
            // Get active weekly session
            var weeklySession = await _votesRepo.GetActiveWeeklySessionAsync()
                ?? throw new ArgumentException($"There is no active weekly voting session.");

            return weeklySession;
        }

        public async Task<DailyVote> GetDailyVoteByIdAsync(Guid dailyVoteId)
        {
            // Get daily vote by specificated id
            var dailyVote = await _votesRepo.GetDailyVoteByIdAsync(dailyVoteId)
                ?? throw new ArgumentException($"Daily vote with id {dailyVoteId} not found.");

            return dailyVote;
        }

        public async Task<WeeklyVotingSession> GetWeeklySessionByIdAsync(Guid weeklySessionId)
        {
            // Get weekly session by specificated id
            var weeklySession = await _votesRepo.GetWeeklySessionByIdAsync(weeklySessionId)
                ?? throw new ArgumentException($"Weekly session with id {weeklySessionId} not found.");

            return weeklySession;
        }

        public async Task<DailyVotingSession> GetDailySessionByIdAsync(Guid dailySessionId)
        {
            // Get daily session by specified id
            var dailySession = await _votesRepo.GetDailySessionByIdAsync(dailySessionId)
                ?? throw new ArgumentException($"Daily session with id {dailySessionId} not found.");

            return dailySession;
        }

        public async Task VoteCoinAsync(Guid dailySessionId, Guid userId, Guid coinId)
        {
            var userExists = await _usersService.UserExistsAsync(userId);
            if(!userExists)
                throw new ArgumentException($"User with ID {userId} does not exist.");

            var sessionExists = await _votesRepo.DailySessionExistsAsync(dailySessionId);
            if(!sessionExists)
                throw new ArgumentException($"Daily voting session with ID {dailySessionId} does not exist.");

            var hasVotedInSession = await _votesRepo.HasUserVotedInDailySessionAsync(dailySessionId, userId);
            if (hasVotedInSession)
                throw new ArgumentException($"User already voted in daily voting session with ID {dailySessionId}.");

            var vote = new Vote
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                CoinId = coinId,
                DailyVotingSessionId = dailySessionId,
                CreatedAt = DateTime.UtcNow
            };

            await _votesRepo.VoteCoinAsync(vote);
        }

        public async Task UnvoteCoinAsync(Guid dailySessionId, Guid userId, Guid coinId)
        {
            var isVoted = await _votesRepo.IsCoinVotedAsync(dailySessionId, userId, coinId);
            if (!isVoted)
                throw new ArgumentException($"Coin with ID {coinId} is not voted.");

            await _votesRepo.UnvoteCoinAsync(dailySessionId, userId, coinId);
        }

        public async Task<bool> IsCoinVotedAsync(Guid dailySessionId, Guid userId, Guid coinId)
        {
            return await _votesRepo.IsCoinVotedAsync(dailySessionId, userId, coinId);
        }

        public async Task CreateWeeklySessionAsync()
        {
            var query = new CoinQueryObject
            {
                SortBy = new List<CoinsSortOption>
                {
                    new() {Field = "likes", Descending = true},
                    new() {Field = "createdAt", Descending = true}
                },
                PageNumber = 1,
                PageSize = 8
            };
            

            var paginatedCoins = await _coinsService.GetAllCoinsAsync(query);

            if (paginatedCoins.TotalItems < 8)
            {
                throw new ArgumentException("Not enough coins created for weekly voting");
            }

            var today = DateTime.UtcNow;
            //if (await _context.DailyVotingSessions.AnyAsync(dvs => dvs.StartDate == today)) return;\

            var weeklySession = new WeeklyVotingSession
            {
                Id = Guid.NewGuid(),
                IsActive = true,
                StartDate = today.Date.Add(new TimeSpan(0,0,0)),
                EndDate = today.Date.AddDays(6).Add(new TimeSpan(23,59,59)),
            };

            List<DailyVotingSession> dailySessions = new();
            List<DailyVote> dailyVotes = new();

            for (var daysCount = 0; daysCount < 7; daysCount++)
            {
                var dailySession = new DailyVotingSession
                {
                    Id = Guid.NewGuid(),
                    IsActive = false,
                    StartDate = today.Date.AddDays(daysCount),
                    EndDate = today.Date.AddDays(daysCount).Add(new TimeSpan(23,59,59)),
                    WeeklyVotingSessionId = weeklySession.Id
                };

                if (daysCount == 0)
                {
                    dailySession.IsActive = true;
                }

                var dailyVote = new DailyVote
                {
                    Id = Guid.NewGuid(),
                    DailyVotingSessionId = dailySession.Id,
                };

                if (daysCount < 4)
                {
                    dailyVote.Coins = paginatedCoins.Items.Skip(daysCount * 2).Take(2).ToList();
                }
                dailySession.DailyVoteId = dailyVote.Id;

                dailySessions.Add(dailySession);
                dailyVotes.Add(dailyVote);
            }

            await _votesRepo.CreateWeeklySessionAsync(weeklySession, dailyVotes, dailySessions);
        }

        public async Task CalculateDailySessionResultAsync()
        {
            /*
            var dailySession = await _votesRepo.GetActiveDailySessionAsync();

            var coins = dailySession.DailyVote.Coins;

            var winner = coins.MaxBy(c => c.VotesCount)
                ?? throw new ArgumentException("There is no winner in the daily voting session");

            dailySession.WinnerId = winner.Id;

            await _votesRepo.SaveChangesAsync();
            await _votesRepo.DeactivateExpiredDailySessionsAsync();
            */
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