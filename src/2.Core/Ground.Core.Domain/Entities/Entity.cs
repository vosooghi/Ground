using Ground.Core.Domain.ValueObjects;

namespace Ground.Core.Domain.Entities
{
    /// <summary>
    /// Represents the base class for an entity in the domain model.
    /// An entity is an object that is defined by its identity rather than its attributes.
    /// </summary>
    public abstract class Entity<TId> : IAuditableEntity
              where TId : struct,
              IComparable,
              IComparable<TId>,
              IConvertible,
              IEquatable<TId>,
              IFormattable
    {
        /// <summary>
        /// Entity Id for SQL Server.
        /// This Id is used for saving and retrieving the entity in/from the database.
        /// </summary>
        public TId Id { get; protected set; }

        /// <summary>
        /// Business Id for the entity.
        /// The entity is identified by BusinessId, and all relations are implemented by BusinessId.
        /// </summary>
        public BusinessId BusinessId { get; protected set; } = BusinessId.FromGuid(Guid.NewGuid());

        /// <summary>
        /// Constructurs with parameters should be accessible from outside of an entity, as entity properties must be filled when the entity is created.
        /// When working with ORMs, we need to have default construcure, so we define it with Protected or Private access modifier.
        /// </summary>
        protected Entity() { }


        #region Equality Check
        public bool Equals(Entity<TId>? other) => other is not null && this == other;
        public override bool Equals(object? obj) =>
             obj is Entity<TId> otherObject && Id.Equals(otherObject.Id);

        public override int GetHashCode() => Id.GetHashCode();
        public static bool operator ==(Entity<TId> left, Entity<TId> right)
        {
            if (left is null && right is null)
                return true;

            if (left is null || right is null)
                return false;

            return left.Equals(right);
        }

        public static bool operator !=(Entity<TId> left, Entity<TId> right)
            => !(right == left);

        #endregion
    }

    /// <summary>
    /// Represents the base class for an entity in the domain model with long Id.
    /// </summary>
    public abstract class Entity : Entity<long>
    {

    }
}
