using AutoMapper;
using MetaCoins.API.Dtos.CoinDtos;
using MetaCoins.Core.Entities;

namespace MetaCoins.API.Helpers
{
    public class CoinProfile : Profile
    {
        public CoinProfile()
        {
            CreateMap<Coin, CoinResponseDto>()
                .ForMember(t => t.CreatedAt, o => o.MapFrom(s => s.CreatedAt.ToString()))
                .ForMember(t => t.ImageUrl, o => o.MapFrom<CoinImageUrlResolver>());

            CreateMap<CoinOwnerRecord, CoinOwnerRecordResponseDto>()
                .ForMember(t => t.AcquiredAt, o => o.MapFrom(s => s.AcquiredAt.ToString()));
        }
    }
}