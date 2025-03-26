using AutoMapper;
using MetaCoins.API.Dtos.WalletDtos;
using MetaCoins.Core.Entities;

namespace MetaCoins.API.Helpers
{
    public class WalletProfile : AutoMapper.Profile
    {
        public WalletProfile()
        {
            CreateMap<Wallet, WalletResponseDto>()
                .ForMember(d => d.CreatedAt, o => o.MapFrom(s => s.CreatedAt.ToString()))
                .ForMember(t => t.OwnerUsername, o => o.MapFrom(s => s.User.UserName));            
        }
    }
}