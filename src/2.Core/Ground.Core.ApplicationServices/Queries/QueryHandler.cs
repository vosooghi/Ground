using Ground.Core.Contracts.ApplicationServices.Queries;
using Ground.Core.RequestResponse.Common;
using Ground.Core.RequestResponse.Queries;
using Ground.Utilities;

namespace Ground.Core.ApplicationServices.Queries
{

    public abstract class QueryHandler<TQuery, TData> : IQueryHandler<TQuery, TData>
        where TQuery : class, IQuery<TData>
    {
        protected readonly GroundServices _groundServices;
        protected readonly QueryResult<TData> result = new();

        protected virtual Task<QueryResult<TData>> ResultAsync(TData data, ApplicationServiceStatus status)
        {
            result._data = data;
            result.Status = status;
            return Task.FromResult(result);
        }

        protected virtual QueryResult<TData> Result(TData data, ApplicationServiceStatus status)
        {
            result._data = data;
            result.Status = status;
            return result;
        }

        protected virtual Task<QueryResult<TData>> ResultAsync(TData data)
        {
            var status = data != null ? ApplicationServiceStatus.Ok : ApplicationServiceStatus.NotFound;
            return ResultAsync(data, status);
        }

        protected virtual QueryResult<TData> Result(TData data)
        {
            var status = data != null ? ApplicationServiceStatus.Ok : ApplicationServiceStatus.NotFound;
            return Result(data, status);
        }

        public QueryHandler(GroundServices groundServices)
        {
            _groundServices = groundServices;
        }

        protected void AddMessage(string message)
        {
            result.AddMessage(_groundServices.Translator[message]);
        }

        protected void AddMessage(string message, params string[] arguments)
        {
            result.AddMessage(_groundServices.Translator[message, arguments]);
        }

        public abstract Task<QueryResult<TData>> Handle(TQuery query);
    }
}
