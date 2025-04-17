using MetaCoins.Core.Interfaces.Services;
using Quartz;

namespace MetaCoins.BLL.Jobs.Voting
{
    public class CreateWeeklySessionJob : IJob
    {
        private readonly IVotesService _votesService;
        public CreateWeeklySessionJob(IVotesService votesService)
        {
            _votesService = votesService;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            await _votesService.CreateWeeklySessionAsync();

            // Logger
            Console.WriteLine("Weekly voting session is created successfully at " + DateTime.UtcNow);
        }
    }
}