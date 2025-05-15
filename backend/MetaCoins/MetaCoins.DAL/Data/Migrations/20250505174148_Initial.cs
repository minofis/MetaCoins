using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MetaCoins.DAL.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CoinPurchaseRequestStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoinPurchaseRequestStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CoinSellOrderStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoinSellOrderStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CoinStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoinStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CoinTransactionStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoinTransactionStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CoinTransactionTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoinTransactionTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Profiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VotingTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VotingTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Wallets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Balance = table.Column<decimal>(type: "numeric", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    WalletId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProfileId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Wallets_WalletId",
                        column: x => x.WalletId,
                        principalTable: "Wallets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Coins",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: false),
                    CoinStatusId = table.Column<int>(type: "integer", nullable: false),
                    Prompt = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    WalletId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Coins_CoinStatuses_CoinStatusId",
                        column: x => x.CoinStatusId,
                        principalTable: "CoinStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Coins_Wallets_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Wallets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Coins_Wallets_WalletId",
                        column: x => x.WalletId,
                        principalTable: "Wallets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CoinOwnerRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    WalletId = table.Column<Guid>(type: "uuid", nullable: false),
                    CoinId = table.Column<Guid>(type: "uuid", nullable: false),
                    AcquiredAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoinOwnerRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CoinOwnerRecords_Coins_CoinId",
                        column: x => x.CoinId,
                        principalTable: "Coins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CoinOwnerRecords_Wallets_WalletId",
                        column: x => x.WalletId,
                        principalTable: "Wallets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CoinSellOrders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StatusId = table.Column<int>(type: "integer", nullable: false),
                    SellerWalletId = table.Column<Guid>(type: "uuid", nullable: false),
                    CoinId = table.Column<Guid>(type: "uuid", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoinSellOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CoinSellOrders_CoinSellOrderStatuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "CoinSellOrderStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CoinSellOrders_Coins_CoinId",
                        column: x => x.CoinId,
                        principalTable: "Coins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CoinSellOrders_Wallets_SellerWalletId",
                        column: x => x.SellerWalletId,
                        principalTable: "Wallets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Likes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CoinId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Likes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Likes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Likes_Coins_CoinId",
                        column: x => x.CoinId,
                        principalTable: "Coins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VotingSessions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    VotingTypeId = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ParentVotingSessionId = table.Column<Guid>(type: "uuid", nullable: true),
                    WinnerId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VotingSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VotingSessions_Coins_WinnerId",
                        column: x => x.WinnerId,
                        principalTable: "Coins",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_VotingSessions_VotingSessions_ParentVotingSessionId",
                        column: x => x.ParentVotingSessionId,
                        principalTable: "VotingSessions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_VotingSessions_VotingTypes_VotingTypeId",
                        column: x => x.VotingTypeId,
                        principalTable: "VotingTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CoinPurchaseRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StatusId = table.Column<int>(type: "integer", nullable: false),
                    CoinSellOrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    BuyerWalletId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RespondedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoinPurchaseRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CoinPurchaseRequests_CoinPurchaseRequestStatuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "CoinPurchaseRequestStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CoinPurchaseRequests_CoinSellOrders_CoinSellOrderId",
                        column: x => x.CoinSellOrderId,
                        principalTable: "CoinSellOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CoinPurchaseRequests_Wallets_BuyerWalletId",
                        column: x => x.BuyerWalletId,
                        principalTable: "Wallets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CoinVotes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CoinId = table.Column<Guid>(type: "uuid", nullable: false),
                    VotingSessionId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoinVotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CoinVotes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CoinVotes_Coins_CoinId",
                        column: x => x.CoinId,
                        principalTable: "Coins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CoinVotes_VotingSessions_VotingSessionId",
                        column: x => x.VotingSessionId,
                        principalTable: "VotingSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CoinVotingSessions",
                columns: table => new
                {
                    CoinId = table.Column<Guid>(type: "uuid", nullable: false),
                    VotingSessionId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoinVotingSessions", x => new { x.CoinId, x.VotingSessionId });
                    table.ForeignKey(
                        name: "FK_CoinVotingSessions_Coins_CoinId",
                        column: x => x.CoinId,
                        principalTable: "Coins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CoinVotingSessions_VotingSessions_VotingSessionId",
                        column: x => x.VotingSessionId,
                        principalTable: "VotingSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CoinTransactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TypeId = table.Column<int>(type: "integer", nullable: false),
                    StatusId = table.Column<int>(type: "integer", nullable: false),
                    CoinId = table.Column<Guid>(type: "uuid", nullable: false),
                    CoinSellOrderId = table.Column<Guid>(type: "uuid", nullable: true),
                    CoinPurchaseRequestId = table.Column<Guid>(type: "uuid", nullable: true),
                    SenderWalletId = table.Column<Guid>(type: "uuid", nullable: true),
                    RecipientWalletId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoinTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CoinTransactions_CoinPurchaseRequests_CoinPurchaseRequestId",
                        column: x => x.CoinPurchaseRequestId,
                        principalTable: "CoinPurchaseRequests",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CoinTransactions_CoinSellOrders_CoinSellOrderId",
                        column: x => x.CoinSellOrderId,
                        principalTable: "CoinSellOrders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CoinTransactions_CoinTransactionStatuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "CoinTransactionStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CoinTransactions_CoinTransactionTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "CoinTransactionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CoinTransactions_Coins_CoinId",
                        column: x => x.CoinId,
                        principalTable: "Coins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CoinTransactions_Wallets_RecipientWalletId",
                        column: x => x.RecipientWalletId,
                        principalTable: "Wallets",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CoinTransactions_Wallets_SenderWalletId",
                        column: x => x.SenderWalletId,
                        principalTable: "Wallets",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("58874eb2-13a9-423e-950d-571b65771274"), null, "Admin", "ADMIN" },
                    { new Guid("b0b3914f-f7ad-4386-80a7-68bf45c776fa"), null, "Customer", "CUSTOMER" }
                });

            migrationBuilder.InsertData(
                table: "CoinPurchaseRequestStatuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Pending" },
                    { 2, "Approved" },
                    { 3, "Rejected" },
                    { 4, "Cancelled" }
                });

            migrationBuilder.InsertData(
                table: "CoinSellOrderStatuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Active" },
                    { 2, "Completed" },
                    { 3, "Cancelled" }
                });

            migrationBuilder.InsertData(
                table: "CoinStatuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Draft" },
                    { 2, "Public" },
                    { 3, "Deleted" }
                });

            migrationBuilder.InsertData(
                table: "CoinTransactionStatuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Pending" },
                    { 2, "Completed" },
                    { 3, "Failed" }
                });

            migrationBuilder.InsertData(
                table: "CoinTransactionTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Transfer" },
                    { 2, "Sale" }
                });

            migrationBuilder.InsertData(
                table: "VotingTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Daily" },
                    { 2, "Weekly" },
                    { 3, "Custom" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ProfileId",
                table: "AspNetUsers",
                column: "ProfileId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_WalletId",
                table: "AspNetUsers",
                column: "WalletId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CoinOwnerRecords_CoinId",
                table: "CoinOwnerRecords",
                column: "CoinId");

            migrationBuilder.CreateIndex(
                name: "IX_CoinOwnerRecords_WalletId",
                table: "CoinOwnerRecords",
                column: "WalletId");

            migrationBuilder.CreateIndex(
                name: "IX_CoinPurchaseRequests_BuyerWalletId",
                table: "CoinPurchaseRequests",
                column: "BuyerWalletId");

            migrationBuilder.CreateIndex(
                name: "IX_CoinPurchaseRequests_CoinSellOrderId",
                table: "CoinPurchaseRequests",
                column: "CoinSellOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_CoinPurchaseRequests_StatusId",
                table: "CoinPurchaseRequests",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Coins_CoinStatusId",
                table: "Coins",
                column: "CoinStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Coins_CreatorId",
                table: "Coins",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Coins_WalletId",
                table: "Coins",
                column: "WalletId");

            migrationBuilder.CreateIndex(
                name: "IX_CoinSellOrders_CoinId",
                table: "CoinSellOrders",
                column: "CoinId");

            migrationBuilder.CreateIndex(
                name: "IX_CoinSellOrders_SellerWalletId",
                table: "CoinSellOrders",
                column: "SellerWalletId");

            migrationBuilder.CreateIndex(
                name: "IX_CoinSellOrders_StatusId",
                table: "CoinSellOrders",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_CoinTransactions_CoinId",
                table: "CoinTransactions",
                column: "CoinId");

            migrationBuilder.CreateIndex(
                name: "IX_CoinTransactions_CoinPurchaseRequestId",
                table: "CoinTransactions",
                column: "CoinPurchaseRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_CoinTransactions_CoinSellOrderId",
                table: "CoinTransactions",
                column: "CoinSellOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_CoinTransactions_RecipientWalletId",
                table: "CoinTransactions",
                column: "RecipientWalletId");

            migrationBuilder.CreateIndex(
                name: "IX_CoinTransactions_SenderWalletId",
                table: "CoinTransactions",
                column: "SenderWalletId");

            migrationBuilder.CreateIndex(
                name: "IX_CoinTransactions_StatusId",
                table: "CoinTransactions",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_CoinTransactions_TypeId",
                table: "CoinTransactions",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CoinVotes_CoinId",
                table: "CoinVotes",
                column: "CoinId");

            migrationBuilder.CreateIndex(
                name: "IX_CoinVotes_UserId",
                table: "CoinVotes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CoinVotes_VotingSessionId",
                table: "CoinVotes",
                column: "VotingSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_CoinVotingSessions_VotingSessionId",
                table: "CoinVotingSessions",
                column: "VotingSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_CoinId",
                table: "Likes",
                column: "CoinId");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_UserId",
                table: "Likes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_VotingSessions_ParentVotingSessionId",
                table: "VotingSessions",
                column: "ParentVotingSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_VotingSessions_VotingTypeId",
                table: "VotingSessions",
                column: "VotingTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_VotingSessions_WinnerId",
                table: "VotingSessions",
                column: "WinnerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CoinOwnerRecords");

            migrationBuilder.DropTable(
                name: "CoinTransactions");

            migrationBuilder.DropTable(
                name: "CoinVotes");

            migrationBuilder.DropTable(
                name: "CoinVotingSessions");

            migrationBuilder.DropTable(
                name: "Likes");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "CoinPurchaseRequests");

            migrationBuilder.DropTable(
                name: "CoinTransactionStatuses");

            migrationBuilder.DropTable(
                name: "CoinTransactionTypes");

            migrationBuilder.DropTable(
                name: "VotingSessions");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "CoinPurchaseRequestStatuses");

            migrationBuilder.DropTable(
                name: "CoinSellOrders");

            migrationBuilder.DropTable(
                name: "VotingTypes");

            migrationBuilder.DropTable(
                name: "Profiles");

            migrationBuilder.DropTable(
                name: "CoinSellOrderStatuses");

            migrationBuilder.DropTable(
                name: "Coins");

            migrationBuilder.DropTable(
                name: "CoinStatuses");

            migrationBuilder.DropTable(
                name: "Wallets");
        }
    }
}
