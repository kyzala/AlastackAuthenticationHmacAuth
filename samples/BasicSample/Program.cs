using Alastack.Authentication.Hawk;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.UseDefaultServiceProvider(options => options.ValidateScopes = true);

builder.Services.AddControllers();
builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = HawkDefaults.AuthenticationScheme;
    })
    .AddHawk(options => 
    {
        var credentials = new HawkCredentials 
        { 
            AuthId = "id123", 
            AuthKey = "abc1234567890", 
            Algorithm = "SHA256",
            RequireServerAuthorization = true,
            IncludeResponsePayloadHash = true,
        };
        options.EnableServerAuthorization = true;
        var dict = new Dictionary<string, HawkCredentials> { { "id123", credentials } };
        options.CredentialsProvider = new MemoryHawkCredentialsProvider(dict);
        options.Events.OnSetSpecificData = context => { context.Data = "spcific data"; return Task.CompletedTask; };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHawkServerAuthorization();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
