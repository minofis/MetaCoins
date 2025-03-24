namespace MetaCoins.Core.Entities.Helpers
{
    public class QueryObject
    {
        public string Username { get; set; } = string.Empty;
        public string SortBy { get; set; } = "likes";
        public bool Descending { get; set; } = false;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}