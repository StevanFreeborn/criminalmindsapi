using MongoDB.Driver;
using server.Models;

namespace server.Persistence.Repositories
{
    public class SeasonRepository : ISeasonRepository
    {
        private readonly IDbContext _context;

        public SeasonRepository(IDbContext context)
        {
            _context = context;
        }

        public async Task<List<Season>> GetSeasonsAsync()
        {
            try
            {
                return await _context.Seasons.Find(season => true).ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<Season> GetSeasonByNumberAsync(int number)
        {
            try
            {
                return await _context.Seasons.Find(season => season.SeasonNumber == number).SingleOrDefaultAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
