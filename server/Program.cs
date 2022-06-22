using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Options;
using server.Filters;
using server.Options;
using server.Persistence;
using server.Persistence.Repositories;
using server.Persistence.Seed;
using System.Reflection;
using AspNetCoreRateLimit;

if (args.Length == 2 && args[0].ToLower() == "seed")
{
    var seeder = new Seeder();

    if (args[1].ToLower() == "seasons")
    {
        await seeder.SeedSeasonsAsync();
    }

    if (args[1].ToLower() == "episodes")
    {
        await seeder.SeedEpisodesAsync();
    }

    if (args[1].ToLower() == "quotes")
    {
        await seeder.SeedQuotesAsync();
    }
}

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMemoryCache();

builder.Services.Configure<IpRateLimitOptions>(
    builder.Configuration.GetSection("IpRateLimiting"));

builder.Services.Configure<IpRateLimitPolicies>(
    builder.Configuration.GetSection("IpRateLimitPolicies"));

builder.Services.AddInMemoryRateLimiting();

builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

builder.Services.Configure<DatabaseSettings>(
    builder.Configuration.GetSection("MongoDBSettings"));

builder.Services.AddSingleton<IDatabaseSettings>(sp => 
    sp.GetRequiredService<IOptions<DatabaseSettings>>().Value);

builder.Services.AddSingleton<IDbContext, DbContext>();

builder.Services.AddScoped<ISeasonRepository, SeasonRepository>();

builder.Services.AddScoped<IEpisodeRepository, EpisodeRepository>();

builder.Services.AddScoped<IQuoteRepository, QuoteRepository>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    options.IncludeXmlComments(xmlPath);
    options.OperationFilter<ApiVersionOperationFilter>();
});

builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();

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

var app = builder.Build();

app.UseStaticFiles();

app.UseSwagger(options =>
{
    options.RouteTemplate = "docs/{documentName}/docs.json";
});

app.UseSwaggerUI(options =>
{
    var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

    foreach (var description in provider.ApiVersionDescriptions)
    {
        var url = $"/docs/{description.GroupName}/docs.json";
        var name = $"criminalmindsapi v{description.ApiVersion}";

        options.RoutePrefix = "docs";
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

app.Run();


