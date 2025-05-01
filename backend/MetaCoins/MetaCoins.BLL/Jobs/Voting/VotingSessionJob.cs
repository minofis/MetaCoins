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
            var votingSessionIdString = context.JobDetail.JobDataMap.GetString("VotingSessionId");

            if (!Guid.TryParse(votingSessionIdString, out Guid votingSessionId))
            {
                Console.WriteLine("Invalid or missing VotingSessionId");
                return;
            }

            await _votingSessionsService.DeactivateVotingSessionAsync(votingSessionId);

            // Logger
            Console.WriteLine("Expired daily voting sessions are deactivated successfully at " + DateTime.UtcNow);

            await _votingSessionsService.DetermineWinnerAsync(votingSessionId);

            // Logger
            Console.WriteLine("Result of daily voting session is calculated successfully at " + DateTime.UtcNow);
        }
    }
}