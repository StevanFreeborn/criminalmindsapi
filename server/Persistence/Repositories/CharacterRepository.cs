using server.Models;

namespace server.Persistence.Repositories
{
    public class CharacterRepository : ICharacterRepository
    {
        // TODO: Implement getcharactersasync
        public Task<List<Character>> GetCharactersAsync(CharacterFilter filter)
        {
            throw new NotImplementedException();
        }

        // TODO: Implement getcharacterbyidasync
        public Task<Character> GetCharacterByIdAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
