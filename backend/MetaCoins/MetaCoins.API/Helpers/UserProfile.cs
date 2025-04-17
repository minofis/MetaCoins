using AutoMapper;
using MetaCoins.API.Dtos.UserDtos;
using MetaCoins.Core.Entities.Identity;

namespace MetaCoins.API.Helpers
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserEntity, UserResponseDto>();
            
            CreateMap<RoleEntity, RoleResponseDto>();
        }
    }
}