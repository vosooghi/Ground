using Ground.Core.Contracts.Data.Commands;
using Ground.Core.Domain.Entities;
using Ground.Core.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Ground.Infra.Data.Sql.Commands
{

    public class BaseCommandRepository<TEntity, TDbContext> : ICommandRepository<TEntity,long>, IUnitOfWork
        where TEntity : AggregateRoot
        where TDbContext : BaseCommandDbContext
    {
        protected readonly TDbContext _dbContext;

        public BaseCommandRepository(TDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        void ICommandRepository<TEntity,long>.Delete(long id)
        {
            var entity = _dbContext.Set<TEntity>().Find(id);
            _dbContext.Set<TEntity>().Remove(entity);
        }

        void ICommandRepository<TEntity, long>.Delete(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }

        void ICommandRepository<TEntity, long>.DeleteGraph(long id)
        {
            var graphPath = _dbContext.GetIncludePaths(typeof(TEntity));
            IQueryable<TEntity> query = _dbContext.Set<TEntity>().AsQueryable();
            foreach (var item in graphPath)
            {
                query = query.Include(item);
            }
            var entity = query.FirstOrDefault(c => c.Id == id);
            if (entity?.Id > 0)
                _dbContext.Set<TEntity>().Remove(entity);
        }

        TEntity ICommandRepository<TEntity, long>.Get(long id)
        {
            return _dbContext.Set<TEntity>().Find(id);
        }

        public TEntity Get(BusinessId businessId)
        {
            return _dbContext.Set<TEntity>().FirstOrDefault(c => c.BusinessId == businessId);
        }

        TEntity ICommandRepository<TEntity, long>.GetGraph(long id)
        {
            var graphPath = _dbContext.GetIncludePaths(typeof(TEntity));
            IQueryable<TEntity> query = _dbContext.Set<TEntity>().AsQueryable();
            var temp = graphPath.ToList();
            foreach (var item in graphPath)
            {
                query = query.Include(item);
            }
            return query.FirstOrDefault(c => c.Id == id);
        }

        void ICommandRepository<TEntity, long>.Insert(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
        }

        bool ICommandRepository<TEntity, long>.Exists(Expression<Func<TEntity, bool>> expression)
        {
            return _dbContext.Set<TEntity>().Any(expression);
        }

        public TEntity GetGraph(BusinessId businessId)
        {
            var graphPath = _dbContext.GetIncludePaths(typeof(TEntity));
            IQueryable<TEntity> query = _dbContext.Set<TEntity>().AsQueryable();
            var temp = graphPath.ToList();
            foreach (var item in graphPath)
            {
                query = query.Include(item);
            }
            return query.FirstOrDefault(c => c.BusinessId == businessId);
        }

        async Task ICommandRepository<TEntity, long>.InsertAsync(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
        }
        async Task<TEntity> ICommandRepository<TEntity, long>.GetAsync(long id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> GetAsync(BusinessId businessId)
        {
            return await _dbContext.Set<TEntity>().FirstOrDefaultAsync(c => c.BusinessId == businessId);
        }

        async Task<TEntity> ICommandRepository<TEntity, long>.GetGraphAsync(long id)
        {
            var graphPath = _dbContext.GetIncludePaths(typeof(TEntity));
            IQueryable<TEntity> query = _dbContext.Set<TEntity>().AsQueryable();
            foreach (var item in graphPath)
            {
                query = query.Include(item);
            }
            return await query.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<TEntity> GetGraphAsync(BusinessId businessId)
        {
            var graphPath = _dbContext.GetIncludePaths(typeof(TEntity));
            IQueryable<TEntity> query = _dbContext.Set<TEntity>().AsQueryable();
            foreach (var item in graphPath)
            {
                query = query.Include(item);
            }
            return await query.FirstOrDefaultAsync(c => c.BusinessId == businessId);
        }

        async Task<bool> ICommandRepository<TEntity, long>.ExistsAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await _dbContext.Set<TEntity>().AnyAsync(expression);
        }

        public int Commit()
        {
            return _dbContext.SaveChanges();
        }

        public Task<int> CommitAsync()
        {
            return _dbContext.SaveChangesAsync();
        }

        public void BeginTransaction()
        {
            _dbContext.BeginTransaction();
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
