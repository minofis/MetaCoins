using AutoMapper;
using MetaCoins.API.Dtos.LikeDtos;
using MetaCoins.Core.Entities;

namespace MetaCoins.API.Helpers
{
    public class LikeProfile : AutoMapper.Profile
    {
        public LikeProfile()
        {
            CreateMap<Like, LikeResponseDto>()
                .ForMember(t => t.Username, o => o.MapFrom(s => s.User.UserName))
                .ForMember(d => d.CreatedAt, o => o.MapFrom(s => s.CreatedAt.ToString()));
        }
    }
}