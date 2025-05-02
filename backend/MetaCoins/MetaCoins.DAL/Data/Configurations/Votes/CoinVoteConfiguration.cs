using MetaCoins.Core.Entities.Votes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MetaCoins.DAL.Data.Configurations.Votes
{
    public class CoinVoteConfiguration : IEntityTypeConfiguration<CoinVote>
    {
        public void Configure(EntityTypeBuilder<CoinVote> builder)
        {
            // CoinVote to User
            builder 
                .HasOne(cv => cv.User)
                .WithMany(u => u.CoinVotes)
                .HasForeignKey(cv => cv.UserId);

            // CoinVote to VotingSession
            builder 
                .HasOne(cv => cv.VotingSession)
                .WithMany(vs => vs.CoinVotes)
                .HasForeignKey(cv => cv.VotingSessionId);
        }
    }
}