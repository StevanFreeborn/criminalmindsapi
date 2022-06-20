﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace server.Models
{
    public class Season
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("seasonNumber")]
        public int? SeasonNumber { get; set; }

        [BsonElement("numberOfEpisodes")]
        public int? NumberOfEpisodes { get; set; }

        [BsonElement("dateFirstAired")]
        [BsonDateTimeOptions(DateOnly = true)]
        public DateTime? DateFirstAired { get; set; }

        [BsonElement("dateLastAired")]
        [BsonDateTimeOptions(DateOnly = true)]
        public DateTime? DateLastAired { get; set; }
    }
}
