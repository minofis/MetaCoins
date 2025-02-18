using System.ComponentModel.DataAnnotations;

namespace MetaCoins.API.Dtos.TransactionDtos
{
    public class TransactionCreateRequestDto
    {
        [Required]
        public Guid SenderWalletId { get; set; }
        [Required]
        public Guid RecipientWalletId { get; set; }
        [Required]
        public Guid CoinId { get; set; }
    }
}