using Alastack.Authentication.AspNetCore;

namespace Alastack.Authentication.HmacAuth.AspNetCore
{
    /// <summary>
    /// The common implementation of <see cref="ICredentialCache{TCredential}"/>.
    /// </summary>
    /// <typeparam name="TCredential">A credential type.</typeparam>
    public class CredentialCache<TCredential> : ICredentialCache<TCredential>
    {
        private readonly IDataCache _dataCache;

        
        /// <summary>
        /// Initializes a new instance of <see cref="CredentialCache{TCredential}"/>.
        /// </summary>
        /// <param name="dataCache"><see cref="IDataCache"/></param>
        public CredentialCache(IDataCache dataCache) 
        {            
            _dataCache = dataCache;
        }

        /// <inheritdoc />
        public async Task<TCredential?> GetCredentialAsync(string key)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public async Task SetCredentialAsync(string key, TCredential credential, long cacheTime)
        {
            throw new NotImplementedException();
        }
    }
}
