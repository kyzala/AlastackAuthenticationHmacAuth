namespace Alastack.Authentication.EntityFrameworkCore
{
    /// <summary>
    /// EFCore CredentialProvider settings.
    /// </summary>
    public class EFCoreCredentialProviderSettings
    {        
        /// <summary>
        /// Table name.
        /// </summary>
        public string TableName { get; set; } = default!;

        /// <summary>
        /// Credential table key name.
        /// </summary>
        public string KeyName { get; set; } = default!;
    }
}
