using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace mapService.Models
{
    public class GeoPoly
    {
        public string type { get; set; }
        public double[][][][] coordinates { get; set; }
    }

    public class GeoMultipoly
    {
        public string type { get; set; }
        public double[][][][] coordinates { get; set; }
    }

    public class InputObj
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public int branch_id { get; set; }
        public GeoMultipoly GeoMultipoly { get; set; }
        public GeoPoly GeoPoly { get; set; }
    }
}
