using MetaCoins.Core.Interfaces.Services;
using Quartz;

namespace MetaCoins.BLL.Jobs.Voting
{
    public class CalculateDailySessionResultJob : IJob
    {
        private readonly IVotesService _votesService;
        public CalculateDailySessionResultJob(IVotesService votesService)
        {
            _votesService = votesService;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            await _votesService.CalculateDailySessionResultAsync();

            // Logger
            Console.WriteLine("Result of daily voting session is calculated successfully at " + DateTime.UtcNow);
        }
    }
}