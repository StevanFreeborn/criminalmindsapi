using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server.Models
{
    public class EpisodeFilter
    {
        /// <summary>
        /// Use to filter episodes by season number.
        /// </summary>
        public int? Season { get; set; } = null;

        /// <summary>
        /// Use to filter episodes by those who have an air date on or after this value.
        /// </summary>
        public DateTime? StartDate { get; set; } = null;

        /// <summary>
        /// Use to filter episodes by those who have an air date on or before this value.
        /// </summary>
        public DateTime? EndDate { get; set; } = null;

        /// <summary>
        /// Use to filter episodes by episode title.
        /// </summary>
        public string? Title { get; set; } = null;

        /// <summary>
        /// Use to filter episodes by those whose summary contains this value.
        /// </summary>
        public string? SummaryKeyword { get; set; } = null;

        /// <summary>
        /// Use to filter episodes by director's name.
        /// </summary>
        public string? DirectedBy { get; set; } = null;

        /// <summary>
        /// Use to filter episodes by writer's name.
        /// </summary>
        public string? WrittenBy { get; set; } = null;

        /// <summary>
        /// Use to filter episodes by those who have millions of us viewers equal to or more than this value.
        /// </summary>
        public double? ViewersRangeStart { get; set; } = null;

        /// <summary>
        /// Use to filter episodes by those who have millions of us viewers equal to or less than this value.
        /// </summary>
        public double? ViewersRangeEnd { get; set; } = null;
    }
}
