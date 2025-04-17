using AutoMapper;
using MetaCoins.API.Dtos.VotingDtos;
using MetaCoins.Core.Entities.Voting;

namespace MetaCoins.API.Helpers
{
    public class VoteProfile : Profile
    {
        public VoteProfile()
        {
            CreateMap<WeeklyVotingSession, WeeklySessionResponseDto>()
                .ForMember(t => t.EndDate, o => o.MapFrom(s => s.EndDate.ToString()))
                .ForMember(t => t.StartDate, o => o.MapFrom(s => s.StartDate.ToString()))
                .ForMember(t => t.DailySessionIds, o => o.MapFrom(s => s.DailySessions
                    .OrderBy(ds => ds.StartDate)
                    .Select(ds => ds.Id).ToArray()));

            CreateMap<DailyVotingSession, DailySessionResponseDto>()
                .ForMember(t => t.EndDate, o => o.MapFrom(s => s.EndDate.ToString()))
                .ForMember(t => t.StartDate, o => o.MapFrom(s => s.StartDate.ToString()));

            CreateMap<DailyVote, DailyVoteResponseDto>()
                .ForMember(t => t.CoinIds, o => o.MapFrom(s => s.Coins.Select(c => c.Id).ToArray()));
        }
    }
}