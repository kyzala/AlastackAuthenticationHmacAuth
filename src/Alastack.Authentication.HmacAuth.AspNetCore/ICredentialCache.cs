﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alastack.Authentication.HmacAuth.AspNetCore
{
    /// <summary>
    /// A credential cache abstraction.
    /// </summary>
    /// <typeparam name="TCredential">a credential type.</typeparam>
    public interface ICredentialCache<TCredential>
    {

        /// <summary>
        /// Gets a credential with the given id.
        /// </summary>
        /// <param name="key">credential cache key</param>
        /// <returns>The task object representing the asynchronous operation, containing the located value or null.</returns>
        Task<TCredential?> GetCredentialAsync(string key);

        /// <summary>
        /// Sets the credential with the given key.
        /// </summary>
        /// <param name="key">credential cache key</param>
        /// <param name="credential">credential instance.</param>
        /// <param name="cacheTime">credential cache time.</param>
        /// <returns>Task that represents the asynchronous operation.</returns>
        Task SetCredentialAsync(string key, TCredential credential, long cacheTime);
    }
}