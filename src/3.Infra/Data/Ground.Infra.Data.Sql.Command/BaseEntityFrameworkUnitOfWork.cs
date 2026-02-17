using Ground.Core.Contracts.Data.Commands;

namespace Ground.Infra.Data.Sql.Commands
{
    /// <summary>
    /// Represents the base implementation of the Unit of Work pattern for Entity Framework Core.
    /// </summary>
    /// <typeparam name="TDbContext"></typeparam>
    public abstract class BaseEntityFrameworkUnitOfWork<TDbContext> : IUnitOfWork
        where TDbContext : BaseCommandDbContext
    {
        protected readonly TDbContext _dbContext;

        public BaseEntityFrameworkUnitOfWork(TDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void BeginTransaction()
        {
            _dbContext.BeginTransaction();
        }

        public int Commit()
        {
            var result = _dbContext.SaveChanges();
            return result;
        }

        public async Task<int> CommitAsync()
        {
            var result = await _dbContext.SaveChangesAsync();
            return result;
        }

        public void CommitTransaction()
        {
            _dbContext.CommitTransaction();
        }

        public void RollbackTransaction()
        {
            _dbContext.RollbackTransaction();
        }
    }

}
