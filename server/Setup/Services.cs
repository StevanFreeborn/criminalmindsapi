using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Options;
using server.Persistence;
using server.Persistence.Repositories;
using server.SwaggerOptions;
using server.SwaggerOptions.Filters;
using System.Reflection;

namespace server.Setup
{
    public static class Services
    {
        public static void SetupDb(WebApplicationBuilder builder)
        {
            builder.Services.Configure<DatabaseSettings>(
            builder.Configuration.GetSection("MongoDBSettings"));

            builder.Services.AddSingleton<IDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<DatabaseSettings>>().Value);

            builder.Services.AddSingleton<IDbContext, DbContext>();

            builder.Services.AddScoped<ISeasonRepository, SeasonRepository>();

            builder.Services.AddScoped<IEpisodeRepository, EpisodeRepository>();

            builder.Services.AddScoped<IQuoteRepository, QuoteRepository>();

            builder.Services.AddScoped<ICharacterRepository, CharacterRepository>();
        }

        public static void SetupMvC(WebApplicationBuilder builder)
        {
            builder.Services.AddMemoryCache();

            builder.Services.Configure<IpRateLimitOptions>(
                builder.Configuration.GetSection("IpRateLimiting"));

            builder.Services.Configure<IpRateLimitPolicies>(
                builder.Configuration.GetSection("IpRateLimitPolicies"));

            builder.Services.AddInMemoryRateLimiting();

            builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
                config.ApiVersionReader = new HeaderApiVersionReader("x-api-version");
            });

            builder.Services.AddVersionedApiExplorer(config =>
            {
                config.GroupNameFormat = "'v'VVV";
            });
        }

        public static void SetupSwagger(WebApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen(options =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                options.IncludeXmlComments(xmlPath);
                options.OperationFilter<ApiVersionOperationFilter>();
            });

            builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();
        }
    }
}
