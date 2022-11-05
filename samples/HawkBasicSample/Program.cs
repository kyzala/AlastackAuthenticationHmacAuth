using Alastack.Authentication.Hawk;
using System.Net.Security;

// Create an SocketsHttpHandler object
var handler = new SocketsHttpHandler
{
    ConnectTimeout = TimeSpan.FromSeconds(10),
    PooledConnectionLifetime = TimeSpan.FromSeconds(1000),
    SslOptions = new SslClientAuthenticationOptions()
    {
        RemoteCertificateValidationCallback = (sender, certificate, chain, errors) => true,
    },
    UseCookies = false
};

// Create a HawkDelegatingHandler object
var authHandler = new HawkDelegatingHandler("id123", "3@uo45er?")
{
    InnerHandler = handler
};

// Create an HttpClient object
var client = new HttpClient(authHandler, disposeHandler: false)
{
    BaseAddress = new Uri("https://localhost:5001/")
};

// Call asynchronous network methods in a try/catch block to handle exceptions
try
{
    var response = await client.GetAsync("/api/TodoItems");

    response.EnsureSuccessStatusCode();

    string responseBody = await response.Content.ReadAsStringAsync();
    Console.WriteLine(responseBody);
}
catch (HttpRequestException e)
{
    Console.WriteLine("\nException Caught!");
    Console.WriteLine("Message :{0} ", e.Message);
}

// Need to call dispose on the HttpClient and DelegatingHandlers objects
// when done using them, so the app doesn't leak resources
handler.Dispose();
authHandler.Dispose();
client.Dispose();

Console.ReadKey();