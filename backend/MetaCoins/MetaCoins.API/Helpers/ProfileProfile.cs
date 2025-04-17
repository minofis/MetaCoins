using MetaCoins.API.Dtos.ProfileDtos;

namespace MetaCoins.API.Helpers
{
    public class ProfileProfile : AutoMapper.Profile
    {
        public ProfileProfile()
        {
            CreateMap<Core.Entities.Profile, ProfileResponseDto>()
                .ForMember(t => t.Username, o => o.MapFrom(s => s.User.UserName));
        }
    }
}