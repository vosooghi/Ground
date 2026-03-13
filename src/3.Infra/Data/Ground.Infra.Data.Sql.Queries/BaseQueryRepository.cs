using Ground.Core.Contracts.Data.Queries;

namespace Ground.Infra.Data.Sql.Queries
{
    /// <summary>
    /// This class serves as a foundation for creating specific query repositories that interact with a SQL database context.
    /// </summary>
    /// <typeparam name="TDbContext"></typeparam>
    public class BaseQueryRepository<TDbContext> : IQueryRepository
     where TDbContext : BaseQueryDbContext
    {
        protected readonly TDbContext _dbContext;
        public BaseQueryRepository(TDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
