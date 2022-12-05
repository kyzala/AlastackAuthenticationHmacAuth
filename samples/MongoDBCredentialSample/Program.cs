using Alastack.Authentication.HmacAuth.AspNetCore;
using Alastack.Authentication.HmacAuth.MongoDB;

// mongodb://localhost:27017?appname=authsample&directConnection=true&serverSelectionTimeoutMS=2000
// mongodb://authreader:Password@localhost:27017?appname=authsample&directConnection=true&serverSelectionTimeoutMS=2000&authSource=credentialdb

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMemoryCache();
builder.Services.AddControllers();
builder.Services.AddAuthentication()
.AddHawk(options =>
{
    options.CredentialProvider = new MongoDBCredentialProvider<HawkCredential>(
        "mongodb://localhost:27017?appname=authsample&directConnection=true&serverSelectionTimeoutMS=2000",
        "credentialdb",
        "hawk",
        "authId");
})
.AddHmac(options =>
{
    options.CredentialProvider = new MongoDBCredentialProvider<HmacCredential>(
        "mongodb://localhost:27017?appname=authsample&directConnection=true&serverSelectionTimeoutMS=2000",
        "credentialdb", 
        "hmac",
        "appId");
});
//mongodb://authUser:Test2022@localhost:27017/credentials
var app = builder.Build();

app.UseAuthentication();
app.UseHawkServerAuthorization();
app.UseAuthorization();
app.MapControllers();
app.Run();