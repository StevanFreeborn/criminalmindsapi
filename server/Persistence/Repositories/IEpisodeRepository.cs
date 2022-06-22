using server.Models;

namespace server.Persistence.Repositories
{
    public interface IEpisodeRepository
    {
        Task<List<Episode>> GetEpisodesAsync(EpisodeFilter filter);
        Task<Episode> GetEpisodeByNumberAsync(int number);
    }
}
