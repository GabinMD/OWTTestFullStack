using BoatApplication.Infrastructure.DataBase.Extentions;
using BoatApplication.WebApp.Server.Infrastructure.Extentions;
using YamlDotNet.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.AddApplicationServices();
builder.AddInfrastructureServices();
builder.AddWebServices();
builder.AddCORS();
builder.Services.AddControllers();

builder.Services.AddOpenApiDocument();
builder.Services.AddOpenApi();

var app = builder.Build();

app.UseDefaultFiles();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    await app.InitialiseDatabaseAsync();
}

app.UseOpenApi();
app.UseCors();

app.UseHealthChecks("/health");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
