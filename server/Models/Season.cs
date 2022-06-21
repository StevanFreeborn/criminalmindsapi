using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace server.Models
{
    public class Season
    {
        /// <summary>
        /// Identifier for the season.
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        /// <summary>
        /// The season number.
        /// </summary>
        [BsonElement("seasonNumber")]
        public int? SeasonNumber { get; set; }

        /// <summary>
        /// The number of episodes in the season.
        /// </summary>
        [BsonElement("numberOfEpisodes")]
        public int? NumberOfEpisodes { get; set; }

        /// <summary>
        /// The date the season first aired.
        /// </summary>
        [BsonElement("dateFirstAired")]
        [BsonDateTimeOptions(DateOnly = true)]
        public DateTime? DateFirstAired { get; set; }

        /// <summary>
        /// The date the season last aired.
        /// </summary>
        [BsonElement("dateLastAired")]
        [BsonDateTimeOptions(DateOnly = true)]
        public DateTime? DateLastAired { get; set; }
    }
}
