using MetaCoins.BLL.Jobs.Voting;
using Quartz;

namespace MetaCoins.API.Helpers
{
    public static class QuartzJobScheduler
    {
        public static void ConfigureQuartzJobs(this IServiceCollectionQuartzConfigurator q)
        {
            var votingSessionJobKey = new JobKey("VotingSessionJob");
            q.AddJob<VotingSessionJob>(opts => opts.WithIdentity(votingSessionJobKey));
            q.AddTrigger(opts => opts
                .ForJob(votingSessionJobKey)
                .WithIdentity("VotingSessionJob-trigger")
                .WithCronSchedule("59 59 23 * * ?", x => x.InTimeZone(TimeZoneInfo.Utc)));
        }
    }
}