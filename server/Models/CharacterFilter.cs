using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server.Models
{
    public class CharacterFilter
    {
        /// <summary>
        /// Used to filter characters by their name.
        /// </summary>
        public string? Name { get; set; } = null;

        /// <summary>
        /// Used to filter characters by the actor's name.
        /// </summary>
        public string? ActorName { get; set; } = null;

        /// <summary>
        /// Used to filter characters by a season.
        /// </summary>
        public int? Season { get; set; } = null;
    }
}
