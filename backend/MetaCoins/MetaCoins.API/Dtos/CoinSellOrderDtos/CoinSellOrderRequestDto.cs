using System.ComponentModel.DataAnnotations;

namespace MetaCoins.API.Dtos.CoinSellOrderDtos
{
    public class CoinSellOrderRequestDto
    {
        [Required]
        public Guid CoinId { get; set; }
        [Required]
        public decimal Price { get; set; }
    }
}