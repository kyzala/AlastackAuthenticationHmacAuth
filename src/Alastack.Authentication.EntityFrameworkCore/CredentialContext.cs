using Microsoft.EntityFrameworkCore;

namespace Alastack.Authentication.EntityFrameworkCore
{
    public class CredentialContext<TCredential> : DbContext where TCredential : class
    {
        private readonly string _tableName;
        private readonly string _keyName;

        public CredentialContext(DbContextOptions<CredentialContext<TCredential>> options, string tableName, string keyName)
            : base(options)
        {
            _tableName = tableName;
            _keyName = keyName;

            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public DbSet<TCredential> Credentials { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            ConfigureCredentialContext(builder);
        }

        private void ConfigureCredentialContext(ModelBuilder builder)
        {
            builder.Entity<TCredential>().ToTable(_tableName);            
            builder.Entity<TCredential>(b =>
            {
                b.HasKey(_keyName);
            });
        }
    }
}