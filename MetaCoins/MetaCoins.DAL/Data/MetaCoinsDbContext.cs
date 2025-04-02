using MetaCoins.Core.Entities;
using MetaCoins.Core.Entities.Identity;
using MetaCoins.Core.Entities.Lookups.Transaction;
using MetaCoins.Core.Entities.Voting;
using MetaCoins.DAL.Data.Configurations;
using MetaCoins.DAL.Data.Configurations.Identity;
using MetaCoins.DAL.Data.Configurations.Lookups;
using MetaCoins.DAL.Data.Configurations.Voting;
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

        // Voting
        public DbSet<Vote> Votes { get; set; }
        public DbSet<DailyVote> DailyVotes { get; set; }
        public DbSet<DailyVotingSession> DailyVotingSessions { get; set; }
        public DbSet<WeeklyVotingSession> WeeklyVotingSessions { get; set; }

        // Lookups
        public DbSet<TransactionType> TransactionTypes { get; set; }
        public DbSet<TransactionStatus> TransactionStatuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Entities
            modelBuilder.ApplyConfiguration(new WalletConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionConfiguration());
            modelBuilder.ApplyConfiguration(new CoinConfiguration());
            modelBuilder.ApplyConfiguration(new LikeConfiguration());
            
            // Voting
            modelBuilder.ApplyConfiguration(new VoteConfiguration());
            modelBuilder.ApplyConfiguration(new DailyVoteConfiguration());
            modelBuilder.ApplyConfiguration(new DailyVotingSessionConfiguration());
            modelBuilder.ApplyConfiguration(new WeeklyVotingSessionConfiguration());

            // Lookups
            modelBuilder.ApplyConfiguration(new TransactionTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionStatusConfiguration());
            
            // Identity
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
            
            base.OnModelCreating(modelBuilder);
        }
    }
}