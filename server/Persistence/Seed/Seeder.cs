using System.Runtime.CompilerServices;
using System.Text.Json;
using MongoDB.Driver;
using server.Models;

namespace server.Persistence.Seed
{
    public class Seeder
    {
        private readonly IMongoClient _client;
        private readonly IMongoDatabase _database;
        private readonly IConfiguration _config;
        private readonly IMongoCollection<Season> _seasons;
        private readonly IMongoCollection<Episode> _episodes;

        public Seeder()
        {
            _config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            _client = new MongoClient(_config.GetSection("MongoDBSettings:ConnectionString").Value);
            _database = _client.GetDatabase(_config.GetSection("MongoDBSettings:DatabaseName").Value);
            _seasons = _database.GetCollection<Season>(_config.GetSection("MongoDBSettings:SeasonsCollection").Value);
            _episodes = _database.GetCollection<Episode>(_config.GetSection("MongoDBSettings:EpisodesCollection").Value);
        }

        public async Task SeedSeasonsAsync()
        {
            await _seasons.DeleteManyAsync(season => true);

            var fileName = "seasons.json";
            var seasonsFilePath = Path.Combine(AppContext.BaseDirectory, $"Persistence/Seed/Data/{fileName}");
            var seasonsJson = File.ReadAllText(seasonsFilePath);
            var seasons = JsonSerializer.Deserialize<List<Season>>(seasonsJson);
               
            if(seasons != null)
            {
                await _seasons.InsertManyAsync(seasons);
                Console.WriteLine($"Seeded databases with seasons from {fileName}");
            }
        }

        public async Task SeedEpisodesAsync()
        {
            await _episodes.DeleteManyAsync(episode => true);

            var fileName = "episodes.json";
            var episodesFilePath = Path.Combine(AppContext.BaseDirectory, $"Persistence/Seed/Data/{fileName}");
            var episodesJson = File.ReadAllText(episodesFilePath);
            var episodes = JsonSerializer.Deserialize<List<Episode>>(episodesJson);

            if (episodes != null)
            {
                await _episodes.InsertManyAsync(episodes);
                Console.WriteLine($"Seeded databases with episodes from {fileName}");
            }
        }
    }
}
