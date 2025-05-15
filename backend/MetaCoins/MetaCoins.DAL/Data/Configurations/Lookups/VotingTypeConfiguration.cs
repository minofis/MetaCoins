using MetaCoins.Core.Entities.Enums.Vote;
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
                    Enum.GetValues(typeof(VotingTypes))
                        .Cast<VotingTypes>()
                        .Select(e => new VotingType
                        {
                            Id = (int)e,
                            Name = e.ToString()
                        })
                );
        }
    }
}