using MetaCoins.Core.Entities.Voting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MetaCoins.DAL.Data.Configurations.Voting
{
    public class DailyVotingSessionConfiguration : IEntityTypeConfiguration<DailyVotingSession>
    {
        public void Configure(EntityTypeBuilder<DailyVotingSession> builder)
        {
            builder 
                .HasOne(dvs => dvs.WeeklyVotingSession)
                .WithMany(wvs => wvs.DailySessions)
                .HasForeignKey(dvs => dvs.WeeklyVotingSessionId);

            builder 
                .HasOne(dvs => dvs.Winner)
                .WithMany()
                .HasForeignKey(dvs => dvs.WinnerId);

            builder 
                .HasMany(dvs => dvs.Votes)
                .WithOne(v => v.DailyVotingSession)
                .HasForeignKey(v => v.DailyVotingSessionId);
        }
    }
}