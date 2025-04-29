namespace MetaCoins.API.Dtos.WalletDtos
{
    public class WalletResponseDto
    {
        public Guid Id { get; set; }
        public string OwnerUsername { get; set; } = string.Empty;
        public decimal Balance { get; set; }
        public string CreatedAt { get; set; } = string.Empty;
    }
}