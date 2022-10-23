namespace Alastack.Authentication
{
    public class AuthenticationHeader
    {
        public string HeaderName { get; set; } = "Authorization";

        public string Scheme { get; set; } = default!;

        public string Parameter { get; set; } = default!;

        public IDictionary<string, object> Properties { get; } = new Dictionary<string, object>();

        public bool IsGenericHeaderName
        {
            get => HeaderName == "Authorization";
        }
    }
}