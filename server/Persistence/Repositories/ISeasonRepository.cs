using server.Models;

namespace server.Persistence.Repositories
{
    public interface ISeasonRepository
    {
        Task<List<Season>> GetSeasonsAsync();
        Task<Season> GetSeasonByNumberAsync(int number);
    }
}
