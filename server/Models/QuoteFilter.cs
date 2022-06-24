namespace server.Models
{
    public class QuoteFilter
    {
        
        /// <summary>
        /// Use to filter quotes by season.
        /// </summary>
        public int? Season { get; set; } = null;

        /// <summary>
        /// Use to filter quotes by episode.
        /// </summary>
        public int? Episode { get; set; } = null;

        /// <summary>
        /// Use to filter quotes by a key word.
        /// </summary>
        public string? TextKeyword { get; set; } = null;

        /// <summary>
        /// Use to filter quotes by source.
        /// </summary>
        public string? Source { get; set; } = null;

        /// <summary>
        /// Use to filter quote by narrator.
        /// </summary>
        public string? Narrator { get; set; } = null;

    }
}
