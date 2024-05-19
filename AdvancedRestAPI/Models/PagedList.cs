namespace AdvancedRestAPI.Models
{
    public class PagedList<T>
    {
        public List<T> Items { get; private set; }
        public int TotalCount { get; private set; }
        public int PageSize { get; private set; }
        public int CurrentPage { get; private set; }
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);

        public PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            Items = items;
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
        }
    }
}
