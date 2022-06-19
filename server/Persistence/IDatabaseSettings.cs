using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server.Persistence
{
    public interface IDatabaseSettings
    {
        string? ConnectionString { get; set; }
        string? DatabaseName { get; set; }
        string? SeasonsCollection { get; set; }
        string? EpisodesCollection { get; set; }
        string? CharactersCollection { get; set; }
        string? QuotesCollection { get; set; }
    }
}
