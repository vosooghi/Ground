namespace Ground.Core.RequestResponse.Queries
{
    /// <summary>
    /// this is a marker for queries that needs paging.
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    public interface IPageQuery<TData> : IQuery<TData>
    {
        /// <summary>
        /// the number of pages.
        /// </summary>
        public int PageNumber { get; set; }
        /// <summary>
        /// the number of records in each page.
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// to skip some records.
        /// </summary>
        public int SkipCount => (PageNumber - 1) * PageSize;
        /// <summary>
        /// wether returns total count or not
        /// </summary>
        public bool NeedTotalCount { get; set; }
        /// <summary>
        /// sort by specific column
        /// </summary>
        public string SortBy { get; set; }
        public bool SortAscending { get; set; }
    }
}
