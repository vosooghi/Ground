namespace Ground.Core.Domain.ValueObjects
{
    /// <summary>
    /// Defines the base class for value objects.
    /// </summary>
    /// <typeparam name="TValueObject">The type of the value object.</typeparam>
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

        /// <summary>
        /// Returns a hash code for the value object based on its components.
        /// </summary>
        /// <returns>A hash code for the value object.</returns>
        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Select(x => x != null ? x.GetHashCode() : 0)
                .Aggregate((x, y) => x ^ y);
        }

        /// <summary>
        /// Returns the components of the value object that are used for equality comparison.
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
