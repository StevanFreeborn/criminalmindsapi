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

                if (filter?.StartDate != null)
                {
                    query = query.Where(episode => episode.AirDate >= filter.StartDate);
                }

                if (filter?.EndDate != null)
                {
                    query = query.Where(episode => episode.AirDate <= filter.EndDate);
                }

                if (filter?.Title != null)
                {
                    query = query.Where(episode => episode.Title!.ToLower().Contains(filter.Title.ToLower()));
                }

                if (filter?.SummaryKeyword != null)
                {
                    query = query.Where(episode => episode.Summary!.ToLower().Contains(filter.SummaryKeyword.ToLower()));
                }

                if (filter?.DirectedBy != null)
                {
                    query = query.Where(episode => episode.DirectedBy!.ToLower().Contains(filter.DirectedBy.ToLower()));
                }

                if (filter?.WrittenBy != null)
                {
                    query = query.Where(episode => episode.WrittenBy.Any(e => e.ToLower().Contains(filter.WrittenBy.ToLower())));
                }

                if (filter?.ViewersRangeStart != null)
                {
                    query = query.Where(episode => episode.UsViewersInMillions >= filter.ViewersRangeStart);
                }

                if (filter?.ViewersRangeEnd != null)
                {
                    query = query.Where(episode => episode.UsViewersInMillions <= filter.ViewersRangeEnd);
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
