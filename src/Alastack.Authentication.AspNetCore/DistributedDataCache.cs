using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;

namespace Alastack.Authentication.AspNetCore
{
    /// <summary>
    /// The distributing implementation of <see cref="IDataCache"/>.
    /// </summary>
    public class DistributedDataCache : IDataCache
    {
        private readonly IDistributedCache _distributedCache;

        /// <summary>
        /// Initializes a new instance of <see cref="DistributedDataCache"/>.
        /// </summary>
        /// <param name="distributedCache">Represents a distributed cache.</param>
        public DistributedDataCache(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        /// <inheritdoc />
        public async Task<byte[]> GetAsync(string key)
        {
            return await _distributedCache.GetAsync(key);
        }

        /// <inheritdoc />
        public async Task SetAsync(string key, byte[] value, TimeSpan absoluteExpirationRelativeToNow)
        {
            var options = new DistributedCacheEntryOptions().SetSlidingExpiration(absoluteExpirationRelativeToNow);
            await _distributedCache.SetAsync(key, value, options);
        }
    }
}