using server.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace server.Persistence.Repositories
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly IDbContext _context;
        
        public CharacterRepository(IDbContext context)
        {
            _context = context;
        }

        public async Task<List<Character>> GetCharactersAsync(CharacterFilter filter)
        {
            try
            {
                var query = _context.Characters.AsQueryable();

                if (filter?.Name != null)
                {
                    query = query.Where(character => character.FullName.ToLower().Contains(filter.Name.ToLower()));
                }

                if (filter?.ActorName != null)
                {
                    query = query.Where(character => character.ActorFullName.ToLower().Contains(filter.ActorName.ToLower()));
                }

                if (filter?.Season != null)
                {
                    query = query.Where(character => character.Seasons.Any(season => season == filter.Season));
                }

                return await query.ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<Character> GetCharacterByIdAsync(string id)
        {
            try
            {
                return await _context.Characters.Find(character => character.Id == id).SingleOrDefaultAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
