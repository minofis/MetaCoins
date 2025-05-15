using System.ComponentModel.DataAnnotations;

namespace MetaCoins.API.Dtos.CoinTransactionDtos
{
    public class TransferCoinRequestDto
    {
        [Required]
        public string RecipientUsername { get; set; } = string.Empty;
        [Required]
        public Guid CoinId { get; set; }
    }
}