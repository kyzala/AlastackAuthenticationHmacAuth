using Alastack.Authentication.Hawk.AspNetCore;
using Alastack.Authentication.Hmac.AspNetCore;
using Alastack.Authentication.Sql;
using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMemoryCache();
builder.Services.AddControllers();
builder.Services.AddAuthentication()
.AddHawk(options =>
{
    options.ForwardIndex = 4;
    options.EnableServerAuthorization = true;
    options.CredentialProvider = new SqlCredentialProvider<HawkCredential, SqlConnection>(
        builder.Configuration.GetConnectionString("CredentialDbConnection"),
        "SELECT AuthId,AuthKey,HmacAlgorithm,HashAlgorithm,User,EnableServerAuthorization,IncludeResponsePayloadHash FROM HawkCredentials WHERE AuthId = @Id");
    options.Events.OnSetSpecificData = context => { context.Data = "specific data"; return Task.CompletedTask; };
})
.AddHmac(options =>
{
    options.ForwardIndex = 4;
    options.CredentialProvider = new SqlCredentialProvider<HmacCredential, SqlConnection>(
        builder.Configuration.GetConnectionString("CredentialDbConnection"),
        "SELECT AppId,AppKey,HmacAlgorithm,HashAlgorithm FROM HmacCredentials WHERE AppId = @Id");
});

var app = builder.Build();

app.UseAuthentication();
app.UseHawkServerAuthorization();
app.UseAuthorization();
app.MapControllers();
app.Run();