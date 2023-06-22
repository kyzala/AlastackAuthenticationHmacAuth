using System.Security.Cryptography;
using System.Text;

namespace Alastack.Authentication.HmacAuth;

/// <summary>
/// The default implementation of <see cref="ICrypto"/>.
/// </summary>
public class DefaultCrypto : ICrypto
{
    private readonly string _hmacAlgorithmName;
    private readonly string _hashAlgorithmName;
    private readonly byte[] _key;

    /// <summary>
    /// Initializes a new instance of <see cref="DefaultCrypto"/>.
    /// </summary>
    /// <param name="hmacAlgorithmName">The HMAC(Hash-based Message Authentication Code) algorithm name.</param>
    /// <param name="hashAlgorithmName">The hash algorithm name.</param>
    /// <param name="key">The key to use in the HMAC calculation.</param>
    public DefaultCrypto(string hmacAlgorithmName, string hashAlgorithmName, byte[] key)
    {
        _hmacAlgorithmName = hmacAlgorithmName;
        _hashAlgorithmName = hashAlgorithmName;
        _key = key;
    }

    /// <inheritdoc />
    public byte[] CalculateHash(byte[] buffer)
    {
        using var hashAlgorithm = HashAlgorithm.Create(_hashAlgorithmName);
        return hashAlgorithm!.ComputeHash(buffer);
    }

    /// <inheritdoc />
    public string CalculateHash(string input)
    {
        var buffer = Encoding.UTF8.GetBytes(input);
        return Convert.ToBase64String(CalculateHash(buffer));
    }

    /// <inheritdoc />
    public string CalculateMac(string input)
    {
        var buffer = Encoding.UTF8.GetBytes(input);
        return Convert.ToBase64String(CalculateMac(buffer));
    }

    /// <inheritdoc />
    public byte[] CalculateMac(byte[] buffer)
    {
        using var hmac = HMAC.Create(_hmacAlgorithmName);
        hmac!.Key = _key;
        return hmac.ComputeHash(buffer);
    }
}