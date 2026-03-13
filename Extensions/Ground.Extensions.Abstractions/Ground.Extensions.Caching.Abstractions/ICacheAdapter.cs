namespace Ground.Extensions.Caching.Abstractions
{
    /// <summary>
    /// Represents a cache adapter interface for adding, retrieving, and removing cached items.
    /// </summary>
    public interface ICacheAdapter
    {
        /// <summary>
        /// Adds an object to the cache with specified expiration settings.
        /// </summary>
        /// <typeparam name="TInput">The type of the object to be cached.</typeparam>
        /// <param name="key">The key under which the object will be cached.</param>
        /// <param name="obj">The object to be cached.</param>
        /// <param name="AbsoluteExpiration">The absolute expiration time for the cached object.</param>
        /// <param name="SlidingExpiration">The sliding expiration time for the cached object.</param>
        void Add<TInput>(string key, TInput obj, DateTime? AbsoluteExpiration, TimeSpan? SlidingExpiration);
        /// <summary>
        /// Retrieves the value associated with the specified key and returns it as the requested type.
        /// </summary>
        /// <typeparam name="TOutput">The type to which the retrieved value will be cast. Must be compatible with the stored value for the
        /// specified key.</typeparam>
        /// <param name="key">The key that identifies the value to retrieve. Cannot be null.</param>
        /// <returns>The value associated with the specified key, cast to <typeparamref name="TOutput"/>. If the key does not
        /// exist, the default value for <typeparamref name="TOutput"/> is returned.</returns>
        TOutput Get<TOutput>(string key);
        /// <summary>
        /// Removes the cached item associated with the specified key.
        /// </summary>
        /// <param name="key">The key that identifies the cached item to remove. Cannot be null.</param>
        void RemoveCache(string key);
    }
}
