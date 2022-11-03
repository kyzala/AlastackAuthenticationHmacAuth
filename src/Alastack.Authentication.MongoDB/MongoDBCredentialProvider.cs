using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using System.Net;

namespace Alastack.Authentication.MongoDB
{
    public class MongoDBCredentialProvider<TCredential> : ICredentialProvider<TCredential>
    {
        public MongoDBCredentialProviderSettings Settings { get; set; } = default!;

        public MongoDBCredentialProvider(MongoDBCredentialProviderSettings settings) => Settings = settings ?? throw new ArgumentNullException(nameof(Settings));

        public virtual async Task<TCredential?> GetCredentialAsync(string id)
        {
            var client = new MongoClient(Settings.ConnectionString);
            var database = client.GetDatabase(Settings.DatabaseName);
            var collection = database.GetCollection<BsonDocument>(Settings.CollectionName);
            var documents = await collection.FindAsync(new BsonDocument(Settings.Id, id));
            var document = await documents.SingleOrDefaultAsync();
            var credential = (TCredential?)BsonTypeMapper.MapToDotNetValue(document);
            return credential;
        }
    }
}