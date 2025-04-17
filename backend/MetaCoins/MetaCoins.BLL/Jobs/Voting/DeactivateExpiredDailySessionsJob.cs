using MetaCoins.Core.Interfaces.Services;
using Quartz;

namespace MetaCoins.BLL.Jobs.Voting
{
    public class DeactivateExpiredDailySessionsJob : IJob
    {
        private readonly IVotesService _votesService;
        public DeactivateExpiredDailySessionsJob(IVotesService votesService)
        {
            _votesService = votesService;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            await _votesService.DeactivateExpiredDailySessionsAsync();

            // Logger
            Console.WriteLine("Expired daily voting sessions are deactivated successfully at " + DateTime.UtcNow);
        }
    }
}