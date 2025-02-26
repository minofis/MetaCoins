using AutoMapper;
using MetaCoins.API.Dtos.WalletDtos;
using MetaCoins.Core.Entities;

namespace MetaCoins.API.Helpers
{
    public class WalletProfile : Profile
    {
        public WalletProfile()
        {
            CreateMap<Wallet, WalletResponseDto>()
                .ForMember(t => t.Status, o => o.MapFrom(s => s.Status.Name))
                .ForMember(t => t.OwnerUsername, o => o.MapFrom(s => s.User.UserName));
            
            CreateMap<WalletDetails, WalletDetailsResponseDto>()
                .ForMember(d => d.CreatedAt, o => o.MapFrom(s => s.CreatedAt.ToString()));
        }
    }
}