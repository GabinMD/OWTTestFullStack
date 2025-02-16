using BoatApplication.Infrastructure.DataBase;

namespace BoatApplication.WebApp.Server.Infrastructure.Extentions
{
    public static class WebApplicationExtentions
    {
        public static void UseOpenApi(this WebApplication app)
        {
            app.MapOpenApi();
            app.UseSwaggerUi(settings =>
            {
                settings.Path = "/api";
                settings.DocumentPath = "/api/specification.json";
            });
        }

        public static void UseCors(this WebApplication app)
        {
            app.UseCors(Constants.WebApp.CorsFrontendPolicy);
        }
    }
}
