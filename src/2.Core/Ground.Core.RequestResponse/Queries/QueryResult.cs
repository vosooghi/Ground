using Ground.Core.RequestResponse.Common;

namespace Ground.Core.RequestResponse.Queries
{
    /// <summary>
    /// the structure of a query result
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    public sealed class QueryResult<TData> : ApplicationServiceResult
    {
        public TData? _data;
        public TData? Data
        {
            get => _data;
        }
    }
}