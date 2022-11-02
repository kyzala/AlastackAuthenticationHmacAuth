namespace Alastack.Authentication.Sql
{
    public class SqlCredentialProviderSettings
    {
        public string ConnectionString { get; set; }

        public string QuerySql { get; set; }

        public string SqlType { get; set; }
    }
}