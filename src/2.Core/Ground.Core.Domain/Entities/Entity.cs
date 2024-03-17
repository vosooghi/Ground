using Ground.Core.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ground.Core.Domain.Entities
{
    /// <summary>
    /// Base Entity Class
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
        /// Entity Id for sql server
        /// For saving and retrieving in/from database
        /// </summary>
        public TId Id { get; protected set; }

        /// <summary>
        /// Entity Id
        /// Entity is identified by this BusinessId
        /// All relations are implemented by BusinessId
        /// </summary>
        public BusinessId BusinessId { get; protected set; } = BusinessId.FromGuid(Guid.NewGuid());

        /// <summary>
        /// Constructurs with parameters should be accessible from outside of an entity, as entity properties must be filled when the entity is created.
        /// When working with ORMs, we need to have default construcure, so we define it with Protected or Private access modifier.
        /// </summary>
        protected Entity() { }


        #region Equality Check
        public bool Equals(Entity<TId>? other) => this == other;
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


    public abstract class Entity : Entity<long>
    {

    }
}
