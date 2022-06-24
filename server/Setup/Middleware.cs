using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace server.Setup
{
    public static class Middleware
    {
        public static WebApplication SetupMiddleware(this WebApplication app)
        {
            app.UseSwagger(options =>
            {
                options.RouteTemplate = "/{documentName}/docs.json";
            });

            app.UseSwaggerUI(options =>
            {
                var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

                foreach (var description in provider.ApiVersionDescriptions)
                {
                    var url = $"/{description.GroupName}/docs.json";
                    var name = $"criminalmindsapi v{description.ApiVersion}";

                    options.RoutePrefix = String.Empty;
                    options.SwaggerEndpoint(url, name);
                    options.EnableTryItOutByDefault();
                    options.DisplayRequestDuration();
                    options.DocumentTitle = "criminalmindsapi";
                }
            });

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.UseIpRateLimiting();

            return app;
        }
    }
}
