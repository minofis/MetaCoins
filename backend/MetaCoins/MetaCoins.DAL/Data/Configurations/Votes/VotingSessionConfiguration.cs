using MetaCoins.Core.Entities.Votes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MetaCoins.DAL.Data.Configurations.Votes
{
    public class VotingSessionConfiguration : IEntityTypeConfiguration<VotingSession>
    {
        public void Configure(EntityTypeBuilder<VotingSession> builder)
        {
            // VotingSession to CoinVotingSessions
            builder 
                .HasMany(vs => vs.CoinVotingSessions)
                .WithOne(cvs => cvs.VotingSession)
                .HasForeignKey(cvs => cvs.VotingSessionId);
            
            // VotingSession to CoinVotes
            builder 
                .HasMany(vs => vs.CoinVotes)
                .WithOne(cv => cv.VotingSession)
                .HasForeignKey(cv => cv.VotingSessionId);

            // VotingSession to SubVotingSessions
            builder 
                .HasMany(vs => vs.SubVotingSessions)
                .WithOne(svs => svs.ParentVotingSession)
                .HasForeignKey(svs => svs.ParentVotingSessionId);

            // VotingSession to Winner
            builder 
                .HasOne(vs => vs.Winner)
                .WithMany()
                .HasForeignKey(vs => vs.WinnerId);
        }
    }
}