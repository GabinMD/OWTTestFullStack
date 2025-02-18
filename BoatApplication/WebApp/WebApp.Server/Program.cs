using BoatApplication.Domain;
using BoatApplication.Infrastructure.DataBase.Extentions;
using BoatApplication.WebApp.Server.Infrastructure.Extentions;
using NSwag;
using NSwag.Generation.Processors.Security;
using YamlDotNet.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.AddDomainServices();
builder.AddApplicationServices();
builder.AddInfrastructureServices();
builder.AddWebServices();
builder.AddCORS();
builder.Services.AddControllers();

builder.Services.AddOpenApiDocument(
    options =>
    {
        options.AddSecurity("Bearer", Enumerable.Empty<string>(), new OpenApiSecurityScheme
        {
            Type = NSwag.OpenApiSecuritySchemeType.ApiKey,
            Name = "Authorization",
            In = NSwag.OpenApiSecurityApiKeyLocation.Header,
            Description = "Type into the textbox: Bearer {your JWT token}.",
            Scheme = "Bearer"
        });

        options.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("Bearer"));
    });
builder.Services.AddOpenApi();

var app = builder.Build();

app.UseDefaultFiles();
if (app.Environment.IsDevelopment())
{
    await app.InitialiseDatabaseAsync();
}

app.UseOpenApi();
app.UseCors();

app.UseHealthChecks("/health");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseExceptionHandler(options => { });

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
