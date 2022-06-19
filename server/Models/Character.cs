using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace server.Models
{
    public class Character
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("firstName")]
        public string? FirstName { get; set; }

        [BsonElement("lastName")]
        public string? LastName { get; set; }

        [BsonElement("actorFirstName")]
        public string? ActorFirstName { get; set; }

        [BsonElement("actorLastName")]
        public string? ActorLastName { get; set; }

        [BsonElement("seasons")]
        public int[]? Seasons { get; set; }

        [BsonElement("firstEpisode")]
        public string? FirstEpisode { get; set; }

        [BsonElement("lastEpisode")]
        public string? LastEpisode { get; set; }

        [BsonElement("image")]
        public string? Image { get; set; }
    }
}
