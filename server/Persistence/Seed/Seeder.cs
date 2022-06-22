﻿using MongoDB.Driver;
using server.Models;
using System.Text.Json;

namespace server.Persistence.Seed
{
    public class Seeder
    {
        private readonly IMongoCollection<Season> _seasons;
        private readonly IMongoCollection<Episode> _episodes;
        private readonly IMongoCollection<Quote> _quotes;

        public Seeder()
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            IMongoClient client = new MongoClient(config.GetSection("MongoDBSettings:ConnectionString").Value);

            IMongoDatabase database = client.GetDatabase(config.GetSection("MongoDBSettings:DatabaseName").Value);
            
            _seasons = database.GetCollection<Season>(config.GetSection("MongoDBSettings:SeasonsCollection").Value);
            _episodes = database.GetCollection<Episode>(config.GetSection("MongoDBSettings:EpisodesCollection").Value);
            _quotes = database.GetCollection<Quote>(config.GetSection("MongoDBSettings:QuotesCollection").Value);
        }

        public async Task SeedSeasonsAsync()
        {
            await _seasons.DeleteManyAsync(season => true);

            const string fileName = "seasons.json";
            var seasonsFilePath = Path.Combine(AppContext.BaseDirectory, $"Persistence/Seed/Data/{fileName}");
            var seasonsJson = await File.ReadAllTextAsync(seasonsFilePath);
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

            const string fileName = "episodes.json";
            var episodesFilePath = Path.Combine(AppContext.BaseDirectory, $"Persistence/Seed/Data/{fileName}");
            var episodesJson = await File.ReadAllTextAsync(episodesFilePath);
            var episodes = JsonSerializer.Deserialize<List<Episode>>(episodesJson);

            if (episodes != null)
            {
                await _episodes.InsertManyAsync(episodes);
                Console.WriteLine($"Seeded databases with episodes from {fileName}");
            }
        }

        public async Task SeedQuotesAsync()
        {
            await _quotes.DeleteManyAsync(quotes => true);

            const string fileName = "quotes.json";
            var quotesFilePath = Path.Combine(AppContext.BaseDirectory, $"Persistence/Seed/Data/{fileName}");
            var quotesJson = await File.ReadAllTextAsync(quotesFilePath);
            var quotes = JsonSerializer.Deserialize<List<Episode>>(quotesJson);

            if (quotes != null)
            {
                await _episodes.InsertManyAsync(quotes);
                Console.WriteLine($"Seeded databases with quotes from {fileName}");
            }

        }
    }
}
