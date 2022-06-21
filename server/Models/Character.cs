using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace server.Models
{
    public class Character
    {
        /// <summary>
        /// Identifier for the character.
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        /// <summary>
        /// The character's first name.
        /// </summary>
        [BsonElement("firstName")]
        public string? FirstName { get; set; }

        /// <summary>
        /// The character's last name.
        /// </summary>
        [BsonElement("lastName")]
        public string? LastName { get; set; }

        /// <summary>
        /// The first name of the actor who potrayed the character.
        /// </summary>
        [BsonElement("actorFirstName")]
        public string? ActorFirstName { get; set; }

        /// <summary>
        /// The last name of the actor who potrayed the character.
        /// </summary>
        [BsonElement("actorLastName")]
        public string? ActorLastName { get; set; }

        /// <summary>
        /// The seasons in which the character appeared.
        /// </summary>
        [BsonElement("seasons")]
        public int[]? Seasons { get; set; }

        /// <summary>
        /// The first episode where the character appeared.
        /// </summary>
        [BsonElement("firstEpisode")]
        public string? FirstEpisode { get; set; }

        /// <summary>
        /// The last episode where the character appeared.
        /// </summary>
        [BsonElement("lastEpisode")]
        public string? LastEpisode { get; set; }

        /// <summary>
        /// A link to an image of the character.
        /// </summary>
        [BsonElement("image")]
        public string? Image { get; set; }
    }
}
