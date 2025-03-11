using System.ComponentModel.DataAnnotations;

namespace MetaCoins.API.Dtos.TransactionDtos
{
    public class TransactionCreateRequestDto
    {
        [Required]
        public Guid SenderWalletId { get; set; }
        [Required]
        public string RecipientUsername { get; set; }
        [Required]
        public Guid CoinId { get; set; }
    }
}