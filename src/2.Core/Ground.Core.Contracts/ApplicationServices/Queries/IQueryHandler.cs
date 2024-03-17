using Ground.Core.RequestResponse.Queries;

namespace Ground.Core.Contracts.ApplicationServices.Queries
{
    /// <summary>
    /// The structure for procsessing a query.
    /// </summary>
    /// <typeparam name="TQuery">Query Type and its parameters</typeparam>
    /// <typeparam name="TData">Return Type</typeparam>
    public interface IQueryHandler<TQuery, TData>
        where TQuery : class, IQuery<TData>
    {
        Task<QueryResult<TData>> Handle(TQuery request);
    }
}
