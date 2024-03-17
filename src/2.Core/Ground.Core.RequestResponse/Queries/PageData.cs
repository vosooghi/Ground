namespace Ground.Core.RequestResponse.Queries
{
    public class PageData<T>
    {
        public List<T> QueryResult { get; set; }
        public int PageNmuber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int TotalCount { get; set; }
    }
}
