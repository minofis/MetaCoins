using MetaCoins.API.Dtos.TransactionDtos;
using MetaCoins.Core.Entities;

namespace MetaCoins.API.Helpers
{
    public class TransactionProfile : AutoMapper.Profile
    {
        public TransactionProfile()
        {
            CreateMap<Transaction, TransactionResponseDto>()
                .ForMember(t => t.Type, o => o.MapFrom(s => s.Type.Name))
                .ForMember(t => t.Status, o => o.MapFrom(s => s.Status.Name))
                .ForMember(t => t.CreatedAt, o => o.MapFrom(s => s.CreatedAt.ToString()));
            
            CreateMap<TransactionCreateRequestDto, Transaction>();
        }
    }
}