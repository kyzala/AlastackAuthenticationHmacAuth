using Dapper;
using System.Data;

namespace Alastack.Authentication.Sql
{
    public class SqlCredentialProvider<TCredential, TConnection> : ICredentialProvider<TCredential> where TConnection : IDbConnection, new()
    {
        public string _connectionString { get; }
        private readonly string _sql;

        public SqlCredentialProvider(string connectionString, string sql)
        {
            _connectionString = connectionString;
            _sql = sql;
        }

        public virtual async Task<TCredential?> GetCredentialAsync(string id)
        {
            using var connection = new TConnection { ConnectionString = _connectionString };
            return await connection.QueryFirstOrDefaultAsync<TCredential>(_sql, new { Id = id });
        }
    }
}