using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;

namespace Alastack.Authentication.AspNetCore
{
    /// <summary>
    /// In-memory and distributing implementation of <see cref="IDataCache"/>.
    /// </summary>
    public class CompositeDataCache : IDataCache
    {
        private readonly IMemoryCache? _memoryCache;
        private readonly IDistributedCache? _distributedCache;

        /// <summary>
        /// Initializes a new instance of <see cref="CompositeDataCache"/>.
        /// </summary>
        /// <param name="memoryCache">Represents a local in-memory cache.</param>
        /// <param name="distributedCache">Represents a distributed cache.</param>
        /// <exception cref="ArgumentNullException">At least one argument can not be null.</exception>
        public CompositeDataCache(IMemoryCache? memoryCache, IDistributedCache? distributedCache)
        {
            if (distributedCache == null && memoryCache == null)
            {
                throw new ArgumentNullException($"{nameof(memoryCache)} and {nameof(distributedCache)}", "At least one argument can not be null.");
            }
            _distributedCache = distributedCache;
            _memoryCache = memoryCache;
        }

        /// <inheritdoc />
        public async Task<byte[]> GetAsync(string key)
        {
            if (_distributedCache != null)
            {
                return await _distributedCache.GetAsync(key);
            }
            return _memoryCache!.Get<byte[]>(key);
        }

        /// <inheritdoc />
        public async Task SetAsync(string key, byte[] value, TimeSpan absoluteExpirationRelativeToNow)
        {
            if (_distributedCache != null)
            {
                var options = new DistributedCacheEntryOptions().SetSlidingExpiration(absoluteExpirationRelativeToNow);
                await _distributedCache.SetAsync(key, value, options);
            }
            _memoryCache!.Set(key, value, absoluteExpirationRelativeToNow);
        }
    }
}