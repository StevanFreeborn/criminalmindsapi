using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace server.Models
{
    public class Episode
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("season")]
        public int? Season { get; set; }

        [BsonElement("numberInSeries")]
        public int? NumberInSeries { get; set; }

        [BsonElement("numberInSeason")]
        public int? NumberInSeason { get; set; }

        [BsonElement("title")]
        public string? Title { get; set; }

        [BsonElement("summary")]
        public string? Summary { get; set; }

        [BsonElement("directedBy")]
        public string? DirectedBy { get; set; }

        [BsonElement("writtenBy")]
        public string[]? WrittenBy { get; set; }

        [BsonElement("airDate")]
        [BsonDateTimeOptions(DateOnly = true)]
        public DateTime? AirDate { get; set; }

        [BsonElement("usViewersInMillions")]
        public double? UsViewersInMillions { get; set; }

    }
}
