using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.OpenApi.Writers;
using MongoDB.Driver;
using server.Models;

namespace server.Persistence
{
    public class DbContext : IDbContext
    {
        public IMongoCollection<Season> Seasons { get; set; }

        public IMongoCollection<Episode> Episodes { get; set; }

        public IMongoCollection<Character> Characters { get; set; }

        public IMongoCollection<Quote> Quotes { get; set; }


        public DbContext(IDatabaseSettings settings)
        {
            IMongoClient client = new MongoClient(settings.ConnectionString);
            IMongoDatabase database = client.GetDatabase(settings.DatabaseName);

            Seasons = database.GetCollection<Season>(settings.SeasonsCollection);
            Episodes = database.GetCollection<Episode>(settings.EpisodesCollection);
            Characters = database.GetCollection<Character>(settings.CharactersCollection);
            Quotes = database.GetCollection<Quote>(settings.QuotesCollection);
        }


    }
}
