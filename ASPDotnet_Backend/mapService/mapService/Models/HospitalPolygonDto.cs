using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace mapService.Models
{
    public class HospitalPolygonDto
    {

        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonIgnoreIfNull]
        public string polygon_name { get; set; }

        [BsonIgnoreIfNull]
        public GeoMultipoly GeoMultipoly { get; set; }

        [BsonIgnoreIfNull]
        public GeoPoly GeoPoly { get; set; }

        [BsonExtraElements]
        [BsonIgnoreIfNull]
        public BsonDocument extra { get; set; }
    }
}
