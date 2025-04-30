using MetaCoins.Core.Entities.Votes;
using MetaCoins.Core.Interfaces.Repositories;
using MetaCoins.Core.Interfaces.Services;

namespace MetaCoins.BLL.Services
{
    public class VotingSessionsService : IVotingSessionsService
    {
        private readonly IVotingSessionsRepository _votingSessionsRepo;
        public VotingSessionsService(IVotingSessionsRepository votingSessionsRepo)
        {
            _votingSessionsRepo = votingSessionsRepo;
        }
        public async Task<List<VotingSession>> GetActiveVotingSessionsAsync()
        {
            var votingSessions = await _votingSessionsRepo.GetActiveVotingSessionsAsync()
                ?? new List<VotingSession>();

            return votingSessions;
        }

        public async Task<VotingSession> GetVotingSessionByIdAsync(Guid votingSessionId)
        {
            var votingSession = await _votingSessionsRepo.GetVotingSessionByIdAsync(votingSessionId)
                ?? throw new ArgumentException($"Voting session with id {votingSessionId} not found.");

            return votingSession;
        }

        public async Task<bool> VotingSessionExistsAsync(Guid votingSessionId)
        {
            return await _votingSessionsRepo.VotingSessionExistsAsync(votingSessionId);
        }

        public async Task DeactivateExpiredVotingSessionsAsync()
        {
            await _votingSessionsRepo.DeactivateExpiredVotingSessionsAsync();
        }
        public Task CreateVotingSessionAsync()
        {
            throw new NotImplementedException();
            /*
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
            */
        }
        public Task DetermineWinnerAsync()
        {
            throw new NotImplementedException();
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
    }
}