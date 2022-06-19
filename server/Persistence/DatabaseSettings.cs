using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server.Persistence
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string? ConnectionString { get; set; }
        public string? DatabaseName { get; set; }
        public string? SeasonsCollection { get; set; }
        public string? EpisodesCollection { get; set; }
        public string? CharactersCollection { get; set; }
        public string? QuotesCollection { get; set; }
    }
}
