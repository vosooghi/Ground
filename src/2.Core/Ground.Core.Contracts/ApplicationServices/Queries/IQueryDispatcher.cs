using Ground.Core.RequestResponse.Queries;

namespace Ground.Core.Contracts.ApplicationServices.Queries
{
    /// <summary>
    /// Implementing Mediator pattern to conntect queries to handlers.
    /// </summary>
    public interface IQueryDispatcher
    {
        Task<QueryResult<TData>> Execute<TQuery, TData>(TQuery query) where TQuery : class, IQuery<TData>;
    }
}
