﻿namespace Alastack.Authentication.MongoDB
{
    /// <summary>
    /// MongoDB CredentialProvider settings.
    /// </summary>
    public class MongoDBCredentialProviderSettings
    {
        /// <summary>
        /// Database connection string.
        /// </summary>
        public string ConnectionString { get; set; } = default!;

        /// <summary>
        /// Database name.
        /// </summary>
        public string DatabaseName { get; set; } = default!;

        /// <summary>
        /// Collection name.
        /// </summary>
        public string CollectionName { get; set; } = default!;

        /// <summary>
        /// Credential id field name.
        /// </summary>
        public string KeyName { get; set; } = default!;
    }
}