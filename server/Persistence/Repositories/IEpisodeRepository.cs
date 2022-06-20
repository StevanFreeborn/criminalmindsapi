using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using server.Models;

namespace server.Persistence.Repositories
{
    public interface IEpisodeRepository
    {
        Task<List<Episode>> GetEpisodesAsync(EpisodeFilter filter);
    }
}
