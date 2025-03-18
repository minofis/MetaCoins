using AutoMapper;
using MetaCoins.API.Dtos.ProfileDtos;
using MetaCoins.Core.Entities;

namespace MetaCoins.API.Helpers
{
    public class ProfileProfile : AutoMapper.Profile
    {
        public ProfileProfile()
        {
            CreateMap<Core.Entities.Profile, ProfileResponseDto>();
        }
    }
}