using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Alastack.Authentication.MongoDB
{
    /// <summary>
    /// The MongoDB implementation of <see cref="ICredentialProvider{TCredential}"/>.
    /// </summary>
    /// <typeparam name="TCredential"></typeparam>
    public class MongoDBCredentialProvider<TCredential> : ICredentialProvider<TCredential>
    {
        /// <summary>
        /// <see cref="MongoDBCredentialProviderSettings"/>.
        /// </summary>
        public MongoDBCredentialProviderSettings Settings { get; set; }

        /// <summary>
        /// Initializes a new instance of <see cref="MongoDBCredentialProvider{TCredential}"/>.
        /// </summary>
        /// <param name="connectionString">Database connection string.</param>
        /// <param name="databaseName">Database name.</param>
        /// <param name="collectionName">Collection name.</param>
        /// <param name="keyName">Credential id field name.</param>
        public MongoDBCredentialProvider(string connectionString, string databaseName, string collectionName, string keyName)
            : this(new MongoDBCredentialProviderSettings { ConnectionString = connectionString, DatabaseName = databaseName, CollectionName = collectionName, KeyName = keyName })
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="MongoDBCredentialProvider{TCredential}"/>.
        /// </summary>
        /// <param name="settings"><see cref="MongoDBCredentialProviderSettings"/>.</param>
        public MongoDBCredentialProvider(MongoDBCredentialProviderSettings settings)
        {
            Settings = settings;
        }

        /// <inheritdoc />
        public virtual async Task<TCredential?> GetCredentialAsync(string id)
        {
            var client = new MongoClient(Settings.ConnectionString);
            var database = client.GetDatabase(Settings.DatabaseName);
            var collection = database.GetCollection<BsonDocument>(Settings.CollectionName);
            var documents = await collection.FindAsync(new BsonDocument(Settings.KeyName, id));
            var document = await documents.SingleOrDefaultAsync();
            //var credential = (TCredential?)BsonTypeMapper.MapToDotNetValue(document);
            var credential = BsonSerializer.Deserialize<TCredential?>(document);
            return credential;
        }
    }
}