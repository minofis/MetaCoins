namespace MetaCoins.DAL.Helpers
{
    public class JwtConfiguration
    {
        public string SecretKey { get; set; } = string.Empty;
        public int ExpiresHours { get; set; }
    }
}