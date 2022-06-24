using server.Models;

namespace server.Persistence.Repositories
{
    public interface ICharacterRepository
    {
        Task<List<Character>> GetCharactersAsync(CharacterFilter filter);
        Task<Character> GetCharacterByIdAsync(string id);
    }
}
