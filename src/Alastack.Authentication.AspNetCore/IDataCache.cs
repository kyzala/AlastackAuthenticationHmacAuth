namespace Alastack.Authentication.AspNetCore
{
    /// <summary>
    /// A data cache abstraction.
    /// </summary>
    public interface IDataCache
    {
        /// <summary>
        /// Gets a value with the given key.
        /// </summary>
        /// <param name="key"> A string identifying the requested value.</param>
        /// <returns>Task that represents the asynchronous operation, containing the located value or null.</returns>
        Task<byte[]> GetAsync(string key);

        /// <summary>
        /// Sets the value with the given key.
        /// </summary>
        /// <param name="key">A string identifying the requested value.</param>
        /// <param name="value">The value to set in the cache.</param>
        /// <param name="absoluteExpirationRelativeToNow">Absolute expiration relative to now.</param>
        /// <returns>Task that represents the asynchronous operation.</returns>
        Task SetAsync(string key, byte[] value, TimeSpan absoluteExpirationRelativeToNow);
    }
}