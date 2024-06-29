using Ground.Core.Domain.Entities;
using Ground.Core.Domain.ValueObjects;
using System.Linq.Expressions;

namespace Ground.Core.Contracts.Data.Commands
{

    /// <summary>
    /// the structure of main functionalities of Command Repository.
    /// </summary>
    /// <typeparam name="TEntity">Aggregate Root. a command repository is instanciated according to an AggregateRoot</typeparam>
    public interface ICommandRepository<TEntity, TId> : IUnitOfWork
        where TEntity : AggregateRoot<TId>
         where TId : struct,
              IComparable,
              IComparable<TId>,
              IConvertible,
              IEquatable<TId>,
              IFormattable
    {
        /// <summary>
        /// Delete on entity with specified Id
        /// </summary>
        /// <param name="id">Id</param>
        void Delete(TId id);

        /// <summary>
        /// Delete an entity with specified Id and it's sub entities.
        /// </summary>
        /// <param name="id">Id</param>
        void DeleteGraph(TId id);

        /// <summary>
        /// Delete the given entity
        /// </summary>
        /// <param name="entity">Entity</param>
        void Delete(TEntity entity);

        /// <summary>
        /// Insert the entity into the Database
        /// </summary>
        /// <param name="entity">Entity</param>
        void Insert(TEntity entity);

        /// <summary>
        /// Insert the entity into the Database
        /// </summary>
        /// <param name="entity">Entity</param>
        Task InsertAsync(TEntity entity);

        /// <summary>
        /// Returns an entity with specified Id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Entity</returns>
        TEntity Get(TId id);
        Task<TEntity> GetAsync(TId id);
        TEntity Get(BusinessId businessId);
        Task<TEntity> GetAsync(BusinessId businessId);
        TEntity GetGraph(TId id);
        Task<TEntity> GetGraphAsync(TId id);
        TEntity GetGraph(BusinessId businessId);
        Task<TEntity> GetGraphAsync(BusinessId businessId);
        bool Exists(Expression<Func<TEntity, bool>> expression);
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression);
    }
}
