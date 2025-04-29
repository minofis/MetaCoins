using System.ComponentModel.DataAnnotations;

namespace MetaCoins.API.Dtos.TransactionDtos
{
    public class TransactionCreateRequestDto
    {
        [Required]
        public string SenderUsername { get; set; } = string.Empty;
        [Required]
        public string RecipientUsername { get; set; } = string.Empty;
        [Required]
        public Guid CoinId { get; set; }
    }
}