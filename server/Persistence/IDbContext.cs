using MongoDB.Driver;
using server.Models;

namespace server.Persistence
{
    public interface IDbContext
    {
        public IMongoCollection<Season> Seasons { get; set; }

        public IMongoCollection<Episode> Episodes { get; set; }

        public IMongoCollection<Character> Characters { get; set; }

        public IMongoCollection<Quote> Quotes { get; set; }
    }
}
