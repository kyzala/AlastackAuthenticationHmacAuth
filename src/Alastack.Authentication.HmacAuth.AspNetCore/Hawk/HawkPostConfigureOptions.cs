using Alastack.Authentication.AspNetCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace Alastack.Authentication.HmacAuth.AspNetCore
{
    /// <summary>
    /// Used to setup defaults for the <see cref="HawkOptions"/>.
    /// </summary>
    public class HawkPostConfigureOptions : IPostConfigureOptions<HawkOptions>
    {
        private readonly IDataCache _dataCache;

        /// <summary>
        /// Initializes the <see cref="HawkPostConfigureOptions"/>.
        /// </summary>
        /// <param name="dataCache">The <see cref="IDataCache"/>.</param>
        public HawkPostConfigureOptions(IDataCache dataCache)
        {
            _dataCache = dataCache;
        }

        /// <summary>
        /// Invoked to post configure a <see cref="HawkOptions"/> instance.
        /// </summary>
        /// <param name="name">The name of the <see cref="HawkOptions"/> instance being configured.</param>
        /// <param name="options">The <see cref="HawkOptions"/> instance to configure.</param>
        public void PostConfigure(string name, HawkOptions options)
        {
            if (options.MaxReplayRequestAge > 0 && options.ReplayRequestValidator == null)
            {
                options.ReplayRequestValidator = new ReplayRequestValidator(_dataCache);
            }
            options.CryptoFactory ??= new DefaultCryptoFactory();
            options.AuthorizationParameterExtractor ??= new HawkParameterExtractor();
            options.HostResolver ??= new DefaultHostResolver();
        }
    }
}