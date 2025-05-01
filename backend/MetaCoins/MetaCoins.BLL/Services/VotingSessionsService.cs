using MetaCoins.Core.Entities;
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

        public async Task DeactivateVotingSessionAsync(Guid votingSessionId)
        {
            await _votingSessionsRepo.UpdateVotingSessionStatusAsync(votingSessionId, false);
        }

        public async Task CreateVotingSessionAsync(
            string title, 
            int votingTypeId, 
            DateTime endDate, 
            List<Guid> coinIds, 
            Guid? parentSessionId)
        {
            var now = DateTime.UtcNow;

            var votingSession = new VotingSession
            {
                Id = Guid.NewGuid(),
                Title = title,
                VotingTypeId = votingTypeId,
                IsActive = true,
                StartDate = now,
                EndDate = endDate,
                ParentVotingSessionId = parentSessionId
            };

            await _votingSessionsRepo.CreateVotingSessionAsync(votingSession);

            if (parentSessionId.HasValue && parentSessionId != Guid.Empty)
            {
                var parentSession = await _votingSessionsRepo.GetVotingSessionByIdAsync(parentSessionId.Value);

                parentSession.SubVotingSessions.Add(votingSession);

                await _votingSessionsRepo.UpdateVotingSessionAsync(parentSession);
            }

            if (coinIds?.Any() == true)
            {
                await AddCoinsToVotingSessionAsync(votingSession.Id, coinIds);
            }   
        }

        public async Task DetermineWinnerAsync(Guid votingSessionId)
        {
            var votingSession = await _votingSessionsRepo.GetVotingSessionByIdAsync(votingSessionId);

            var coinVotes = votingSession.CoinVotes;

            var winnerId = coinVotes
                .GroupBy(cv => cv.CoinId)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .FirstOrDefault();

            await _votingSessionsRepo.UpdateVotingSessionWinnerAsync(votingSessionId, winnerId);
        }

        public async Task AddCoinsToVotingSessionAsync(Guid votingSessionId, IEnumerable<Guid> coinIds)
        {
            var coinVotingSessions = coinIds
                .Select(coinId => new CoinVotingSession
                {
                    VotingSessionId = votingSessionId,
                    CoinId = coinId
                })
                .ToList();

            await _votingSessionsRepo.AddCoinsToVotingSessionAsync(coinVotingSessions);
        }
    }
}