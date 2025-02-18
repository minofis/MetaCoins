using MetaCoins.Core.Entities;
using MetaCoins.Core.Interfaces.Repositories;
using MetaCoins.Core.Interfaces.Services;

namespace MetaCoins.BLL.Services
{
    public class CoinsService : ICoinsService
    {
        private readonly ICoinsRepository _coinsRepo;
        public CoinsService(ICoinsRepository coinsRepo)
        {
            _coinsRepo = coinsRepo;
        }
        public async Task CreateCoinAsync(Guid walletId)
        {
            var coin = new Coin
            {
                Id = Guid.NewGuid(),
                WalletId = walletId,
                CreatorId = walletId,
                CreatedAt = DateTime.Now.ToUniversalTime(),
            };
            await _coinsRepo.CreateCoinAsync(coin);

            var ownerRecord = new CoinOwnerRecord{
                Id = Guid.NewGuid(),
                WalletId = walletId,
                CoinId = coin.Id,
                AcquiredAt = DateTime.Now.ToUniversalTime()
            };
            await _coinsRepo.CreateCoinOwnerRecordAsync(ownerRecord);

            coin.OwnershipRecords.Add(ownerRecord);

            await _coinsRepo.SaveChangesAsync();
        }

        public async Task<List<Coin>> GetAllCoinsAsync()
        {
            var coins = await _coinsRepo.GetAllCoinsAsync();

            return coins;
        }

        public async Task<Coin> GetCoinByIdAsync(Guid coinId)
        {
            var coin = await _coinsRepo.GetCoinByIdAsync(coinId)
                ?? throw new ArgumentException($"Coin with ID {coinId} not found.");;

            return coin;
        }

        public async Task<List<CoinOwnerRecord>> GetCoinOwnershipRecordsByIdAsync(Guid coinId)
        {
            var coin = await _coinsRepo.GetCoinByIdAsync(coinId)
                ?? throw new ArgumentException($"Coin with ID {coinId} not found.");;

            var ownerRecords = coin.OwnershipRecords.ToList()
                ?? throw new ArgumentException($"There are no any ownership records.");

            return ownerRecords;
        }
    }
}