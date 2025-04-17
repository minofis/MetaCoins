using MetaCoins.Core.Interfaces.Services;
using Quartz;

namespace MetaCoins.BLL.Jobs.Voting
{
    public class DeactivateExpiredWeeklySessionsJob : IJob
    {
        private readonly IVotesService _votesService;
        public DeactivateExpiredWeeklySessionsJob(IVotesService votesService)
        {
            _votesService = votesService;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            await _votesService.DeactivateExpiredWeeklySessionsAsync();

            // Logger
            Console.WriteLine("Expired weekly voting sessions are deactivated successfully at " + DateTime.UtcNow);
        }
    }
}