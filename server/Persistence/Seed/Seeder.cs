using System.Runtime.CompilerServices;
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

        public async Task SeedAsync()
        {
            var seasons = await Seasons.Find(season => true).ToListAsync();
            foreach (var season in seasons)
            {
                Console.WriteLine(season);
            }
        }
    }
}
