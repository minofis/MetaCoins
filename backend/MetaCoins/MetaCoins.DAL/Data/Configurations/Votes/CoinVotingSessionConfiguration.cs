using MetaCoins.Core.Entities.Votes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace MetaCoins.DAL.Data.Configurations.Votes
{
    public class CoinVotingSessionConfiguration : IEntityTypeConfiguration<CoinVotingSession>
    {
        public void Configure(EntityTypeBuilder<CoinVotingSession> builder)
        {
            // Primary key
            builder
                .HasKey(cvs => new { cvs.CoinId, cvs.VotingSessionId });

            // CoinVotingSession to Coin
            builder
                .HasOne(cvs => cvs.Coin)
                .WithMany(c => c.CoinVotingSessions)
                .HasForeignKey(cvs => cvs.CoinId);

            // CoinVotingSession to VotingSession
            builder
                .HasOne(cvs => cvs.VotingSession)
                .WithMany(vs => vs.CoinVotingSessions)
                .HasForeignKey(cvs => cvs.VotingSessionId);
        }
    }
}