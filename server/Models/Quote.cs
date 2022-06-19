using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace server.Models
{
    public class Quote
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("season")]
        public int? Season { get; set; }

        [BsonElement("episode")]
        public int Episode { get; set; }

        [BsonElement("text")]
        public string? Text { get; set; }

        [BsonElement("source")]
        public string? Source { get; set; }

        [BsonElement("narrator")]
        public string? Narrator { get; set; }
    }
}
