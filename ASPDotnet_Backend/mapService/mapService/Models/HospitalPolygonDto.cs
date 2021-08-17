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
        
        public string polygon_name { get; set; }

        public GeoMultipoly GeoMultipoly { get; set; }

        public GeoPoly GeoPoly { get; set; }

    }
}
