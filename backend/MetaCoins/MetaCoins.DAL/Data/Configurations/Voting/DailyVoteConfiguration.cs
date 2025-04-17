using MetaCoins.Core.Entities.Voting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MetaCoins.DAL.Data.Configurations.Voting
{
    public class DailyVoteConfiguration : IEntityTypeConfiguration<DailyVote>
    {
        public void Configure(EntityTypeBuilder<DailyVote> builder)
        {
            builder 
                .HasOne(dv => dv.DailyVotingSession)
                .WithOne(dvs => dvs.DailyVote)
                .HasForeignKey<DailyVote>(dv => dv.DailyVotingSessionId);

            builder 
                .HasMany(dv => dv.Coins)
                .WithOne();
        }
    }
}