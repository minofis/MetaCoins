using MetaCoins.Core.Interfaces.Services;
using Quartz;

namespace MetaCoins.BLL.Jobs.Voting
{
    public class VotingSessionJob : IJob
    {
        private readonly IVotingSessionsService _votingSessionsService;
        public VotingSessionJob(IVotingSessionsService votingSessionsService)
        {
            _votingSessionsService = votingSessionsService;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            await _votingSessionsService.DeactivateExpiredVotingSessionsAsync();

            // Logger
            Console.WriteLine("Expired daily voting sessions are deactivated successfully at " + DateTime.UtcNow);

            await _votingSessionsService.DetermineWinnerAsync();

            // Logger
            Console.WriteLine("Result of daily voting session is calculated successfully at " + DateTime.UtcNow);
        }
    }
}