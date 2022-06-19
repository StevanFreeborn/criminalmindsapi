using Microsoft.Extensions.Options;
using MongoDB.Driver;
using server.Persistence;
using server.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<DatabaseSettings>(
    builder.Configuration.GetSection("MongoDBSettings"));

builder.Services.AddSingleton<IDatabaseSettings>(sp => 
    sp.GetRequiredService<IOptions<DatabaseSettings>>().Value);

builder.Services.AddSingleton<IDbContext, DbContext>();

builder.Services.AddScoped<ISeasonRepository, SeasonRepository>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
