using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using server.Persistence;
using server.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<DatabaseSettings>(
    builder.Configuration.GetSection("MongoDBSettings"));

builder.Services.AddSingleton<IDatabaseSettings>(sp => 
    sp.GetRequiredService<IOptions<DatabaseSettings>>().Value);

builder.Services.AddSingleton<IDbContext, DbContext>();

builder.Services.AddScoped<ISeasonRepository, SeasonRepository>();

builder.Services.AddScoped<IEpisodeRepository, EpisodeRepository>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "criminalmindsapi", Version = "v1" });
});

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "criminalmindsapi v1");
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
