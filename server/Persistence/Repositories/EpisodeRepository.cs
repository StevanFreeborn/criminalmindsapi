using MongoDB.Driver;
using MongoDB.Driver.Linq;
using server.Models;

namespace server.Persistence.Repositories
{
    public class EpisodeRepository : IEpisodeRepository
    {
        private readonly IDbContext _context;

        public EpisodeRepository(IDbContext context)
        {
            _context = context;
        }

        public async Task<List<Episode>> GetEpisodesAsync(EpisodeFilter? filter)
        {
            try
            {
                var query = _context.Episodes.AsQueryable();

                if (filter?.Season != null)
                {
                    query = query.Where(episode => episode.Season == filter.Season);
                }

                return await query.ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<Episode> GetEpisodeByNumberAsync(int number)
        {
            try
            {
                return await _context.Episodes.Find(episode => episode.NumberInSeries == number).SingleOrDefaultAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
