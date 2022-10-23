using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;

namespace Alastack.Authentication.AspNetCore
{
    /// <summary>
    /// The in-memory implementation of <see cref="IDataCache"/>.
    /// </summary>
    public class MemoryDataCache : IDataCache
    {
        private readonly IMemoryCache _memoryCache;

        /// <summary>
        /// Initializes a new instance of <see cref="MemoryDataCache"/>.
        /// </summary>
        /// <param name="memoryCache">Represents a local in-memory cache.</param>
        public MemoryDataCache(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        /// <inheritdoc />
        public async Task<byte[]> GetAsync(string key)
        {
            var result = _memoryCache.Get<byte[]>(key);
            return await Task.FromResult(result);
        }

        /// <inheritdoc />
        public async Task SetAsync(string key, byte[] value, TimeSpan absoluteExpirationRelativeToNow)
        {
            _memoryCache.Set(key, value, absoluteExpirationRelativeToNow);
            await Task.CompletedTask;
        }
    }
}