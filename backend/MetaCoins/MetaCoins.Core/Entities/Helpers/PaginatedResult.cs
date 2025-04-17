namespace MetaCoins.Core.Entities.Helpers
{
    public class PaginatedResult<T>
    {
        public List<T> Items { get; set; } = new List<T>();
        public int TotalItems { get; set; } 
        public int Page { get; set; } 
        public int PageSize { get; set; } 
        public bool HasNextPage => PageSize * Page < TotalItems;
        public bool HasPreviousPage => Page > 1;
    }
}