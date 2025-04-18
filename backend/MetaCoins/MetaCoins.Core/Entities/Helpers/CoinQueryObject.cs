namespace MetaCoins.Core.Entities.Helpers
{
    public class CoinQueryObject
    {
        public string Username { get; set; } = string.Empty;
        public List<CoinsSortOption> SortBy { get; set; } = new();
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}