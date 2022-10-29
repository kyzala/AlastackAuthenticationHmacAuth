using Alastack.Authentication.AspNetCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace Alastack.Authentication.Hmac.AspNetCore
{
    /// <summary>
    /// Used to setup defaults for the <see cref="HmacOptions"/>.
    /// </summary>
    public class HmacPostConfigureOptions : IPostConfigureOptions<HmacOptions>
    {
        private readonly IDataCache _dataCache;

        /// <summary>
        /// Initializes the <see cref="HmacPostConfigureOptions"/>.
        /// </summary>
        /// <param name="dataCache">The <see cref="IDataCache"/>.</param>
        public HmacPostConfigureOptions(IDataCache dataCache)
        {
            _dataCache = dataCache;
        }

        ///// <summary>
        ///// Initializes the <see cref="HmacPostConfigureOptions"/>.
        ///// </summary>
        ///// <param name="memoryCache">The <see cref="IMemoryCache"/>.</param>
        ///// <param name="distributedCache">The <see cref="IDistributedCache"/>.</param>
        //public HmacPostConfigureOptions(IMemoryCache? memoryCache = null, IDistributedCache? distributedCache = null)
        //{
        //    _dataCache = new DataCache(memoryCache, distributedCache);
        //}

        /// <summary>
        /// Invoked to post configure a <see cref="HmacOptions"/> instance.
        /// </summary>
        /// <param name="name">The name of the <see cref="HmacOptions"/> instance being configured.</param>
        /// <param name="options">The <see cref="HmacOptions"/> instance to configure.</param>
        public void PostConfigure(string name, HmacOptions options)
        {
            if (options.MaxReplayRequestAge > 0 && options.ReplayRequestValidator == null)
            {
                options.ReplayRequestValidator = new ReplayRequestValidator(_dataCache);
            }
            options.CryptoFactory ??= new DefaultCryptoFactory();
            options.AuthorizationParameterExtractor ??= new HmacParameterExtractor();
            options.HostResolver ??= new DefaultHostResolver();
        }
    }
}