namespace Alastack.Authentication.AspNetCore
{
    /// <summary>
    /// A credential provider abstraction.
    /// </summary>
    /// <typeparam name="TCredential">a credential type.</typeparam>
    public interface ICredentialProvider<TCredential>
    {
        /// <summary>
        /// Gets a credential with the given id.
        /// </summary>
        /// <param name="id">credential id</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<TCredential?> GetCredentialAsync(string id);
    }
}