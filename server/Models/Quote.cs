using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace server.Models
{
    public class Quote
    {
        /// <summary>
        /// Identifier for the quote.
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        /// <summary>
        /// The season number during which the quote was said.
        /// </summary>
        [BsonElement("season")]
        public int? Season { get; set; }

        /// <summary>
        /// The episode number during which the quote was said.
        /// </summary>
        [BsonElement("episode")]
        public int Episode { get; set; }

        /// <summary>
        /// The quote text.
        /// </summary>
        [BsonElement("text")]
        public string? Text { get; set; }

        /// <summary>
        /// The source of the quote.
        /// </summary>
        [BsonElement("source")]
        public string? Source { get; set; }

        /// <summary>
        /// The narrator of the quote.
        /// </summary>
        [BsonElement("narrator")]
        public string? Narrator { get; set; }
    }
}
