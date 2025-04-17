using MetaCoins.Core.Entities.Voting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MetaCoins.DAL.Data.Configurations
{
    public class VoteConfiguration : IEntityTypeConfiguration<Vote>
    {
        public void Configure(EntityTypeBuilder<Vote> builder)
        {
            builder 
                .HasOne(v => v.User)
                .WithMany(u => u.Votes)
                .HasForeignKey(v => v.UserId);

            builder 
                .HasOne(v => v.DailyVotingSession)
                .WithMany(dvs => dvs.Votes)
                .HasForeignKey(v => v.DailyVotingSessionId);
        }
    }
}