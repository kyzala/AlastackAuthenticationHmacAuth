using Microsoft.EntityFrameworkCore;

namespace Alastack.Authentication.EntityFrameworkCore
{
    public class EFCoreCredentialProvider<TCredential> : ICredentialProvider<TCredential> where TCredential : class
    {        
        private readonly EFCoreCredentialProviderSettings _settings;
        private readonly DbContextOptions<CredentialContext<TCredential>> _contextOptions;

        public EFCoreCredentialProvider(DbContextOptionsBuilder<CredentialContext<TCredential>> optionsBuilder, string tableName, string keyName)
            : this(optionsBuilder.Options, tableName, keyName)
        {
        }

        public EFCoreCredentialProvider(DbContextOptionsBuilder<CredentialContext<TCredential>> optionsBuilder, EFCoreCredentialProviderSettings settings)
            : this(optionsBuilder.Options, settings)
        {
        }

        public EFCoreCredentialProvider(DbContextOptions<CredentialContext<TCredential>> contextOptions, string tableName, string keyName)
            : this(contextOptions, new EFCoreCredentialProviderSettings { TableName = tableName, KeyName = keyName })
        {
        }

        public EFCoreCredentialProvider(DbContextOptions<CredentialContext<TCredential>> contextOptions, EFCoreCredentialProviderSettings settings)
        {
            _contextOptions = contextOptions;
            _settings = settings;
        }

        public async Task<TCredential?> GetCredentialAsync(string id)
        {
            using var context = new CredentialContext<TCredential>(_contextOptions, _settings.TableName, _settings.KeyName);
            return await context.Credentials.FindAsync(id);
        }
    }
}