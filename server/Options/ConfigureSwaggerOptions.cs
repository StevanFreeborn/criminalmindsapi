using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace server.Options
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
        {
            _provider = provider;
        }

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                var versionInfo = CreateVersionInfo(description);

                options.SwaggerDoc(description.GroupName, versionInfo);
            }
        }

        private static OpenApiInfo CreateVersionInfo(ApiVersionDescription description)
        {
            var info = new OpenApiInfo
            {
                Title = "criminalmindsapi",
                Version = description.ApiVersion.ToString(),
                Description = "An api that provides information about the Criminal Minds series.",
                Contact = new OpenApiContact
                {
                    Name = "Stevan Freeborn",
                    Email = "stevan.freeborn@gmail.com",
                    Url = new Uri("https://stevanfreeborn.com")
                }
            };

            return info;
        }
    }
}
