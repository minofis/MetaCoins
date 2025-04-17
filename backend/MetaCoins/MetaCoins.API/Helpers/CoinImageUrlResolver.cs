using AutoMapper;
using MetaCoins.API.Dtos.CoinDtos;
using MetaCoins.Core.Entities;

namespace MetaCoins.API.Helpers
{
    public class CoinImageUrlResolver : IValueResolver<Coin, CoinResponseDto, string>
    {
        private readonly IConfiguration _config;
        public CoinImageUrlResolver(IConfiguration config)
        {
            _config = config;
        }
        public string Resolve(Coin source, CoinResponseDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.ImageUrl))
            {
                return _config["ApiUrl"] + source.ImageUrl;
            }

            return null;
        }
    }
} 