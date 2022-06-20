using System.Runtime.CompilerServices;
using System.Text.Json;
using MongoDB.Driver;
using server.Models;

namespace server.Persistence.Seed
{
    public class Seeder
    {
        private readonly IMongoClient client;
        private readonly IMongoDatabase database;
        private readonly IConfiguration configuration;
        private readonly IMongoCollection<Season> Seasons;

        public Seeder()
        {
            configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            client = new MongoClient(configuration.GetSection("MongoDBSettings:ConnectionString").Value);
            database = client.GetDatabase(configuration.GetSection("MongoDBSettings:DatabaseName").Value);
            Seasons = database.GetCollection<Season>(configuration.GetSection("MongoDBSettings:SeasonsCollection").Value);
        }

        public async Task SeedSeasonsAsync()
        {
            await Seasons.DeleteManyAsync(season => true);

            var seasonsFilePath = Path.Combine(AppContext.BaseDirectory, "Persistence/Seed/Data/seasons.json");
            var seasonsJson = File.ReadAllText(seasonsFilePath);
            var seasons = JsonSerializer.Deserialize<List<Season>>(seasonsJson);
               
            if(seasons != null)
            {
                await Seasons.InsertManyAsync(seasons);
            }
        }
    }
}
