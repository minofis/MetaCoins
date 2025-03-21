namespace MetaCoins.Core.Entities.Helpers
{
    public class QueryObject
    {
        public string SortBy { get; set; } = "createdAt";
        public bool Descending { get; set; } = false;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}