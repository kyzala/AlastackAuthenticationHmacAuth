# Alastack Authentication HmacAuth

A .NET API supports Hmac and Hawk authentication.

[![GitHub Workflow Status](https://img.shields.io/github/actions/workflow/status/kyzala/AlastackAuthenticationHmacAuth/dotnet.yml?branch=main)](https://github.com/kyzala/AlastackAuthenticationHmacAuth/actions/workflows/dotnet.yml)[![GitHub](https://img.shields.io/github/license/kyzala/AlastackAuthenticationHmacAuth)](LICENSE)

## Getting started

### Install package from the .NET CLI

Client：

```
dotnet add package Alastack.Authentication.HmacAuth
```

AspNetCore：

```
dotnet add package Alastack.Authentication.HmacAuth.AspNetCore
```

### Hmac Authentication

The following code snippet demonstrates creating a Hmac authentication client:

```csharp
var authHandler = new HmacDelegatingHandler("id123", "3@uo45er?")
{
    InnerHandler = new SocketsHttpHandler
    {
        ConnectTimeout = TimeSpan.FromSeconds(10),
        PooledConnectionLifetime = TimeSpan.FromSeconds(1000),
        SslOptions = new SslClientAuthenticationOptions()
        {
            RemoteCertificateValidationCallback = (sender, certificate, chain, errors) => true
        },
        UseCookies = false
    }
};
var client = new HttpClient(authHandler, disposeHandler: false)
{
    BaseAddress = new Uri("https://localhost:5001/")
};
```

The following code snippet demonstrates creating a Hmac authentication client with dependency injection and then invoking it:

```csharp
var host = new HostBuilder()
.ConfigureServices(services =>
{
    services.Configure<HmacSettings>(options =>
    {
        options.AppId = "id123";
        options.AppKey = "3@uo45er?";
    });
    services.AddSingleton<IValidateOptions<HmacSettings>, HmacSettingsValidation>();
    services.AddTransient<HmacDelegatingHandler>();
    services.AddHttpClient<ApiClient>("ApiClient", httpClient => 
        httpClient.BaseAddress = "https://localhost:5001/")
    .AddHttpMessageHandler<HmacDelegatingHandler>();
}).Build();
var apiClient = host.Services.GetRequiredService<ApiClient>();
await apiClient.CreateTodoItemAsync(new TodoItem { Name = "walk dog" });
```

The following code will add Hmac authentication for AspNetCore:

```csharp
builder.Services.AddAuthentication()
.AddHmac(options =>
{
     var credential = new HmacCredential { AppId = "id123", AppKey = "3@uo45er?" };
     var dict = new Dictionary<string, HmacCredential> { { "id123", credential } };
     options.CredentialProvider = new MemoryCredentialProvider<HmacCredential>(dict);
});
```

### Hawk Authentication

You can use [Postman](https://www.postman.com/) as Hawk authentication client. For more information see [Authorizing requests | Postman Learning Center](https://learning.postman.com/docs/sending-requests/authorization/#hawk-authentication).

The following code snippet demonstrates creating a Hawk authentication client:

```csharp
var authHandler = new HawkDelegatingHandler("id123", "3@uo45er?")
{
    InnerHandler = new SocketsHttpHandler
    {
        ConnectTimeout = TimeSpan.FromSeconds(10),
        PooledConnectionLifetime = TimeSpan.FromSeconds(1000),
        SslOptions = new SslClientAuthenticationOptions()
        {
            RemoteCertificateValidationCallback = (sender, certificate, chain, errors) => true
        },
        UseCookies = false
    }
};
var client = new HttpClient(authHandler, disposeHandler: false)
{
    BaseAddress = new Uri("https://localhost:5001/")
};
```

The following code snippet demonstrates creating a Hawk authentication client with dependency injection and then invoking it:

```csharp
var host = new HostBuilder()
.ConfigureServices(services =>
{
    services.Configure<HawkSettings>(options =>
    {
        options.AuthId = "id123";
        options.AuthKey = "3@uo45er?";
    });
    services.AddSingleton<IValidateOptions<HawkSettings>, HawkSettingsValidation>();
    services.AddTransient<HawkDelegatingHandler>();
    services.AddHttpClient<ApiClient>("ApiClient", httpClient => 
        httpClient.BaseAddress = "https://localhost:5001/")
    .AddHttpMessageHandler<HawkDelegatingHandler>();
}).Build();
var apiClient = host.Services.GetRequiredService<ApiClient>();
await apiClient.CreateTodoItemAsync(new TodoItem { Name = "walk dog" });
```

The following code will add Hawk authentication for AspNetCore:

```csharp
builder.Services.AddAuthentication()
.AddHawk(options =>
{
     var credential = new HawkCredential { AuthId = "id123", AuthKey = "3@uo45er?" };
     var dict = new Dictionary<string, HawkCredential> { { "id123", credential } };
     options.CredentialProvider = new MemoryCredentialProvider<HawkCredential>(dict);
});
```

## NuGet Packages

This repo builds the following packages.

| Package                                 | Version                                                      | Description                                                  |
| --------------------------------------- | ------------------------------------------------------------ | ------------------------------------------------------------ |
| Alastack.Authentication                 | [![Nuget](https://img.shields.io/nuget/v/Alastack.Authentication)](https://www.nuget.org/packages/Alastack.Authentication) | Authentication abstraction.                     |
| Alastack.Authentication.AspNetCore      | [![Nuget](https://img.shields.io/nuget/v/Alastack.Authentication.AspNetCore)](https://www.nuget.org/packages/Alastack.Authentication.AspNetCore) | Authentication abstraction for ASP.NET Core. |
| Alastack.Authentication.HmacAuth | [![Nuget](https://img.shields.io/nuget/v/Alastack.Authentication.HmacAuth)](https://www.nuget.org/packages/Alastack.Authentication.HmacAuth) | Hmac and Hawk authentication client.|
| Alastack.Authentication.HmacAuth.AspNetCore | [![Nuget](https://img.shields.io/nuget/v/Alastack.Authentication.HmacAuth.AspNetCore)](https://www.nuget.org/packages/Alastack.Authentication.HmacAuth.AspNetCore) | Hmac and Hawk authentication for ASP.NET Core.|
| Alastack.Authentication.HmacAuth.Sql             | [![Nuget](https://img.shields.io/nuget/v/Alastack.Authentication.HmacAuth.Sql)](https://www.nuget.org/packages/Alastack.Authentication.HmacAuth.Sql) | Sql CredentialProvider.                   |
| Alastack.Authentication.HmacAuth.MongoDB             | [![Nuget](https://img.shields.io/nuget/v/Alastack.Authentication.HmacAuth.MongoDB)](https://www.nuget.org/packages/Alastack.Authentication.HmacAuth.MongoDB) | MongoDB CredentialProvider.                   |
| Alastack.Authentication.HmacAuth.EntityFrameworkCore             | [![Nuget](https://img.shields.io/nuget/v/Alastack.Authentication.HmacAuth.EntityFrameworkCore)](https://www.nuget.org/packages/Alastack.Authentication.HmacAuth.EntityFrameworkCore) | EntityFrameworkCore CredentialProvider.                   |
| Alastack.Authentication.HmacAuth.LiteDB             | [![Nuget](https://img.shields.io/nuget/v/Alastack.Authentication.HmacAuth.LiteDB)](https://www.nuget.org/packages/Alastack.Authentication.HmacAuth.LiteDB) | LiteDB CredentialProvider.                   |

## Configure Authentication

- **Hmac** - `HmacSettings` for authentication client, `HmacOptions` for AspNetCoe.
- **Hawk** - `HawkSettings` for authentication client, `HawkOptions` for AspNetCoe.

## Performance

- `ICredentialCache<TCredential>` defines credential cache abstraction.
- `CredentialCache<TCredential>` is the default implementation.
- `IDataCache` instance stores credential data.
- `CacheKeyPrefix` and `CredentialCacheTime` options configure cache parameters.

## API Extentions

- **`ICrypto`**

A hash algorithms abstraction. `DefaultCrypto` is the default implementation.

- **`ICryptoFactory`**

A factory abstraction for a component that can create ICrypto instances. `DefaultCryptoFactory` is the default implementation.

- **`INonceGenerator`**

A nonce generator abstraction. `NonceGenerator` is the default implementation.

- **`ITimestampCalculator`**

A timestamp calculator abstraction. `TimestampCalculator` is the default implementation.

- **`IAuthorizationParameterExtractor`**

a HTTP Authorization header parameter extractor abstraction. `HmacParameterExtractor` is the Hmac authentication implementation. `HawkParameterExtractor` is the Hawk authentication implementation.

- **`IHostResolver`**

A host resolver abstraction. `DefaultHostResolver` is the default implementation. `DefaultHostResolver` supports forwarded header. `HmacOptions.ForwardIndex` and `HawkOptions.ForwardIndex` is used to set the reverse host index of the forwarding header.

The following HTTP headers display X-Forwarded information.

```http
X-Forwarded-Host: 192.168.1.103, 192.168.1.102:1080, 192.168.1.103:2080, 192.168.1.102, 192.168.1.103:3080
X-Forwarded-Proto: http, http, http, https, http
```

If `ForwardIndex` is `3`, `DefaultHostResolver` will return `192.168.1.102:1080`.

- **`IReplayRequestValidator`**

A HTTP replay request Validator abstraction. `ReplayRequestValidator` is the default implementation.

- **`ICredentialProvider<TCredential>`**

A credential provider abstraction. `MemoryCredentialProvider<TCredential>` is a in-memory implementation.

- **`IDataCache`**

A data cache abstraction. `DataCache` integrates in-memory and distributing cache implementation.

## Samples

Visit the [samples](https://github.com/kyzala/AlastackAuthenticationHmacAuth/tree/main/samples) folder.
