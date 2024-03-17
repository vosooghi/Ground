using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ground.Core.Domain.ValueObjects
{
    /// <summary>
    /// Value Objects for all entities
    /// https://martinfowler.com/bliki/ValueObject.html
    /// </summary>
    /// <typeparam name="TValueObject"></typeparam>
    public abstract class BaseValueObject<TValueObject> : IEquatable<TValueObject>
            where TValueObject : BaseValueObject<TValueObject>
    {
        public bool Equals(TValueObject other) => this == other;

        public override bool Equals(object obj)
        {
            if (obj is TValueObject otherObject)
            {
                return GetEqualityComponents().SequenceEqual(otherObject.GetEqualityComponents());
            }
            return false;
        }
        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Select(x => x != null ? x.GetHashCode() : 0)
                .Aggregate((x, y) => x ^ y);
        }

        /// <summary>
        /// Returens each property for comprising.
        /// </summary>
        /// <returns></returns>
        protected abstract IEnumerable<object> GetEqualityComponents();
        public static bool operator ==(BaseValueObject<TValueObject> right, BaseValueObject<TValueObject> left)
        {
            if (right is null && left is null)
                return true;
            if (right is null || left is null)
                return false;
            return right.Equals(left);
        }
        public static bool operator !=(BaseValueObject<TValueObject> right, BaseValueObject<TValueObject> left) => !(right == left);
    }
}
