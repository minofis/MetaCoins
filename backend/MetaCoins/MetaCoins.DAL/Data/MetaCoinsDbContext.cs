using MetaCoins.Core.Entities;
using MetaCoins.Core.Entities.Identity;
using MetaCoins.Core.Entities.Lookups.Coin;
using MetaCoins.Core.Entities.Lookups.CoinPurchaseRequest;
using MetaCoins.Core.Entities.Lookups.CoinSellOrder;
using MetaCoins.Core.Entities.Lookups.CoinTransaction;
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
        public DbSet<CoinTransaction> CoinTransactions { get; set; }
        public DbSet<CoinPurchaseRequest> CoinPurchaseRequests { get; set; }
        public DbSet<CoinSellOrder> CoinSellOrders { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<CoinOwnerRecord> CoinOwnerRecords { get; set; }

        // Votes
        public DbSet<CoinVote> CoinVotes { get; set; }
        public DbSet<VotingSession> VotingSessions { get; set; }
        public DbSet<CoinVotingSession> CoinVotingSessions { get; set; }

        // Lookups
        public DbSet<CoinTransactionType> CoinTransactionTypes { get; set; }
        public DbSet<CoinTransactionStatus> CoinTransactionStatuses { get; set; }
        public DbSet<CoinSellOrderStatus> CoinSellOrderStatuses { get; set; }
        public DbSet<CoinPurchaseRequestStatus> CoinPurchaseRequestStatuses { get; set; }
        public DbSet<VotingType> VotingTypes { get; set; }
        public DbSet<CoinStatus> CoinStatuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Entities
            modelBuilder.ApplyConfiguration(new WalletConfiguration());
            modelBuilder.ApplyConfiguration(new CoinTransactionConfiguration());
            modelBuilder.ApplyConfiguration(new CoinSellOrderConfiguration());
            modelBuilder.ApplyConfiguration(new CoinPurchaseRequestConfiguration());
            modelBuilder.ApplyConfiguration(new CoinConfiguration());
            modelBuilder.ApplyConfiguration(new LikeConfiguration());
            
            // Votes
            modelBuilder.ApplyConfiguration(new CoinVoteConfiguration());
            modelBuilder.ApplyConfiguration(new VotingSessionConfiguration());
            modelBuilder.ApplyConfiguration(new CoinVotingSessionConfiguration());

            // Lookups
            modelBuilder.ApplyConfiguration(new CoinTransactionTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CoinTransactionStatusConfiguration());
            modelBuilder.ApplyConfiguration(new CoinSellOrderStatusConfiguration());
            modelBuilder.ApplyConfiguration(new CoinPurchaseRequestStatusConfiguration());
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