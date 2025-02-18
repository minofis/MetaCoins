using System.ComponentModel.DataAnnotations;

namespace MetaCoins.API.Dtos.WalletDtos
{
    public class WalletCreateRequestDto
    {
        [Required]
        public string Type { get; set; }
    }
}