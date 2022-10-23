namespace Alastack.Authentication
{
    public static class CryptoDefaults
    {
        public static readonly string[] HMACAlgorithmNames = new[] { "HMACMD5", "HMACSHA1", "HMACSHA256", "HMACSHA384", "HMACSHA512" };

        public static readonly string[] HashAlgorithmNames = new[] { "MD5", "SHA1", "SHA256", "SHA384", "SHA512" };
    }
}