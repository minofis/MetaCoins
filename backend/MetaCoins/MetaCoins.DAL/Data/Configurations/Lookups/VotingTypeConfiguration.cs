using MetaCoins.Core.Entities.Lookups.Votes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MetaCoins.DAL.Data.Configurations.Lookups
{
    public class VotingTypeConfiguration : IEntityTypeConfiguration<VotingType>
    {
        public void Configure(EntityTypeBuilder<VotingType> builder)
        {
            builder
                .HasData(
                    new VotingType{Id = 1, Name = "Daily"},
                    new VotingType{Id = 2, Name = "Weekly"},
                    new VotingType{Id = 3, Name = "Custom"}
                );
        }
    }
}