using MetaCoins.Core.Entities;
using MetaCoins.Core.Entities.Identity;
using MetaCoins.Core.Entities.Lookups.Coin;
using MetaCoins.Core.Entities.Lookups.Transaction;
using MetaCoins.Core.Entities.Lookups.Votes;
using MetaCoins.Core.Entities.Votes;
using MetaCoins.DAL.Data.Configurations;
using MetaCoins.DAL.Data.Configurations.Identity;
using MetaCoins.DAL.Data.Configurations.Lookups;
using MetaCoins.DAL.Data.Configurations.Votes;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MetaCoins.DAL.Data
{
    public class MetaCoinsDbContext : IdentityDbContext<UserEntity, RoleEntity, Guid>
    {
        public MetaCoinsDbContext(DbContextOptions<MetaCoinsDbContext> options) : base(options){}

        // Entities
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Coin> Coins { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<CoinOwnerRecord> CoinOwnerRecords { get; set; }

        // Votes
        public DbSet<CoinVote> CoinVotes { get; set; }
        public DbSet<VotingSession> VotingSessions { get; set; }
        public DbSet<CoinVotingSession> CoinVotingSessions { get; set; }

        // Lookups
        public DbSet<TransactionType> TransactionTypes { get; set; }
        public DbSet<TransactionStatus> TransactionStatuses { get; set; }
        public DbSet<VotingType> VotingTypes { get; set; }
        public DbSet<CoinStatus> CoinStatuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Entities
            modelBuilder.ApplyConfiguration(new WalletConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionConfiguration());
            modelBuilder.ApplyConfiguration(new CoinConfiguration());
            modelBuilder.ApplyConfiguration(new LikeConfiguration());
            
            // Votes
            modelBuilder.ApplyConfiguration(new CoinVoteConfiguration());
            modelBuilder.ApplyConfiguration(new VotingSessionConfiguration());
            modelBuilder.ApplyConfiguration(new CoinVotingSessionConfiguration());

            // Lookups
            modelBuilder.ApplyConfiguration(new TransactionTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionStatusConfiguration());
            modelBuilder.ApplyConfiguration(new VotingTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CoinStatusConfiguration());
            
            // Identity
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
            
            base.OnModelCreating(modelBuilder);
        }
    }
}