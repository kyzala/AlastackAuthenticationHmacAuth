using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alastack.Authentication.MongoDB
{
    public class MongoDBCredentialProviderSettings
    {
        public string ConnectionString { get; set; } = default!;

        public string DatabaseName { get; set; } = default!;

        public string CollectionName { get; set; } = default!;

        public string Id { get; set; } = default!;
    }
}
