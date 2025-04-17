using MetaCoins.Core.Interfaces.Services;
using Quartz;

namespace MetaCoins.BLL.Jobs.Voting
{
    public class ActivateStartedDailySessionsJob : IJob
    {
        private readonly IVotesService _votesService;
        public ActivateStartedDailySessionsJob(IVotesService votesService)
        {
            _votesService = votesService;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            await _votesService.ActivateStartedDailySessionsAsync();

            // Logger
            Console.WriteLine("Started daily voting sessions are activated successfully at " + DateTime.UtcNow);
        }
    }
}