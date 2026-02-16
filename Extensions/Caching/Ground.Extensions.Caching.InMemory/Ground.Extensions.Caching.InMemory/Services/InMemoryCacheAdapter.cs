using Ground.Extensions.Caching.Abstractions;
using Ground.Extensions.Serializers.Abstractions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Ground.Extensions.Caching.InMemory.Services
{
    /// <summary>
    /// Provides an adapter for caching objects in memory using the IMemoryCache interface. Enables storing, retrieving,
    /// and removing cache entries with support for expiration policies.
    /// </summary>
    public class InMemoryCacheAdapter : ICacheAdapter
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IJsonSerializer _jsonSerializer;
        private readonly ILogger<InMemoryCacheAdapter> _logger;

        public InMemoryCacheAdapter(IMemoryCache memoryCache,
                                    IJsonSerializer jsonSerializer,
                                    ILogger<InMemoryCacheAdapter> logger)
        {
            _memoryCache = memoryCache;
            _jsonSerializer = jsonSerializer;
            _logger = logger;
            _logger.LogInformation("InMemoryCache Adapter Start working");
        }

        /// <summary>
        /// Adds an entry to the in-memory cache with the specified key and expiration settings.
        /// </summary>
        /// <remarks>If both absolute and sliding expiration are specified, the cache entry will expire
        /// when either condition is met. The method overwrites any existing entry with the same key.</remarks>
        /// <typeparam name="TInput">The type of the object to be cached.</typeparam>
        /// <param name="key">The unique key used to identify the cached entry.</param>
        /// <param name="obj">The object to store in the cache. Can be of any type.</param>
        /// <param name="absoluteExpiration">The absolute expiration date and time for the cache entry. If null, the entry does not expire based on
        /// absolute time.</param>
        /// <param name="slidingExpiration">The sliding expiration interval for the cache entry. If null, sliding expiration is not applied.</param>
        public void Add<TInput>(string key, TInput obj, DateTime? absoluteExpiration, TimeSpan? slidingExpiration)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Cache key cannot be null or whitespace.", nameof(key));

            if (_logger.IsEnabled(LogLevel.Trace))
            {
                _logger.LogTrace(
                    "InMemoryCache Adapter Cache {Type} with key: {Key}, data: {Data}, absoluteExpiration: {AbsoluteExpiration}, slidingExpiration: {SlidingExpiration}",
                    typeof(TInput).FullName,
                    key,
                    _jsonSerializer.Serialize(obj),
                    absoluteExpiration,
                    slidingExpiration);
            }

            _memoryCache.Set(key, obj, new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = absoluteExpiration,
                SlidingExpiration = slidingExpiration,
            });
        }

        /// <summary>
        /// Retrieves a cached value associated with the specified key and returns it as the requested type.
        /// </summary>
        /// <remarks>If the specified key does not exist in the cache, the method returns the default
        /// value for <typeparamref name="TOutput"/> (for reference types, this is <see langword="null"/>). The method
        /// does not throw an exception if the key is missing.</remarks>
        /// <typeparam name="TOutput">The type of the value to retrieve from the cache.</typeparam>
        /// <param name="key">The key that identifies the cached entry to retrieve.</param>
        /// <returns>The value associated with the specified key if found; otherwise, the default value for <typeparamref
        /// name="TOutput"/>.</returns>
        public TOutput Get<TOutput>(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Cache key cannot be null or whitespace.", nameof(key));

            _logger.LogTrace("InMemoryCache Adapter Try Get Cache with key : {key}", key);

            if (!_memoryCache.TryGetValue(key, out var resultObject))
            {
                _logger.LogTrace("InMemoryCache Adapter Failed Get Cache with key: {Key}", key);
                return default!;
            }

            if (resultObject is TOutput typed)
            {
                if (_logger.IsEnabled(LogLevel.Trace))
                {
                    _logger.LogTrace(
                        "InMemoryCache Adapter Successful Get Cache with key: {Key} and data: {Data}",
                        key,
                        _jsonSerializer.Serialize(resultObject));
                }

                return typed;
            }

            _logger.LogWarning(
                "InMemoryCache Adapter Cache type mismatch for key: {Key}. Requested: {RequestedType}, Actual: {ActualType}",
                key,
                typeof(TOutput).FullName,
                resultObject?.GetType().FullName);

            return default!;
        }

        /// <summary>
        /// Removes the cache entry associated with the specified key from the underlying memory cache.
        /// </summary>
        /// <remarks>If the specified key does not exist in the cache, no action is taken.</remarks>
        /// <param name="key">The key that identifies the cache entry to remove.</param>
        public void RemoveCache(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Cache key cannot be null or whitespace.", nameof(key));

            _logger.LogTrace("InMemoryCache Adapter Remove Cache with key : {key}", key);

            _memoryCache.Remove(key);
        }
    }
}
