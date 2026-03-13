using Ground.Core.RequestResponse.Queries;
using Ground.Utilities.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Ground.Infra.Data.Sql.Queries
{
    /// <summary>
    /// Provides extension methods for repositories to support paginated queries.
    /// </summary>
    public static class QueryRepositoryExtensions
    {
        public static async Task<PageData<TResult>> ToPagedData<TEntity, TQuery, TResult>(this IQueryable<TEntity> entities, PageQuery<PageData<TQuery>> query, Func<TEntity, TResult> selectFunc)
        {
            var result = new PageData<TResult>
            {
                PageNmuber = query.PageNumber,
                PageSize = query.PageSize
            };
            if (query.NeedTotalCount)
                result.TotalCount = await entities.CountAsync();

            if (!string.IsNullOrWhiteSpace(query.SortBy))
                entities = entities.OrderByField(query.SortBy, query.SortAscending);
            entities = entities.Skip(query.SkipCount).Take(query.PageSize);

            result.QueryResult = await entities.Select(
                   c => selectFunc(c)).ToListAsync();
            return result;
        }
    }
}
