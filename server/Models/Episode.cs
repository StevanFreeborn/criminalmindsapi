using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace server.Models
{
    public class Episode
    {
        /// <summary>
        /// Identifier for the episode.
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        /// <summary>
        /// Season number for the episode.
        /// </summary>
        [BsonElement("season")]
        public int? Season { get; set; }

        /// <summary>
        /// The episde number relative to the entire series.
        /// </summary>
        [BsonElement("numberInSeries")]
        public int? NumberInSeries { get; set; }

        /// <summary>
        /// The episode number relative to its season.
        /// </summary>
        [BsonElement("numberInSeason")]
        public int? NumberInSeason { get; set; }

        /// <summary>
        /// The title of the episode.
        /// </summary>
        [BsonElement("title")]
        public string? Title { get; set; }

        /// <summary>
        /// A brief summary of the episode.
        /// </summary>
        [BsonElement("summary")]
        public string? Summary { get; set; }

        /// <summary>
        /// The director of the episode.
        /// </summary>
        [BsonElement("directedBy")]
        public string? DirectedBy { get; set; }

        /// <summary>
        /// The writer of the episode.
        /// </summary>
        [BsonElement("writtenBy")]
        public string[]? WrittenBy { get; set; }

        /// <summary>
        /// Date the episode aired.
        /// </summary>
        [BsonElement("airDate")]
        [BsonDateTimeOptions(DateOnly = true)]
        public DateTime? AirDate { get; set; }

        /// <summary>
        /// Number of us viewers in millions for the episode.
        /// </summary>
        [BsonElement("usViewersInMillions")]
        public double? UsViewersInMillions { get; set; }

    }
}
