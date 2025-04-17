using MetaCoins.Core.Entities.Voting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MetaCoins.DAL.Data.Configurations.Voting
{
    public class WeeklyVotingSessionConfiguration : IEntityTypeConfiguration<WeeklyVotingSession>
    {
        public void Configure(EntityTypeBuilder<WeeklyVotingSession> builder)
        {
            builder 
                .HasMany(wvs => wvs.DailySessions)
                .WithOne(dvs => dvs.WeeklyVotingSession)
                .HasForeignKey(dvs => dvs.WeeklyVotingSessionId);

            builder 
                .HasOne(wvs => wvs.Winner)
                .WithMany()
                .HasForeignKey(wvs => wvs.WinnerId);
        }
    }
}