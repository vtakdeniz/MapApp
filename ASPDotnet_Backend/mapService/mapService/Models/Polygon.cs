using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace mapService.Models
{
    public class GeoPoly
    {
        public string type { get; set; }
        public double[][][] coordinates { get; set; }
    }

    public class GeoMultipoly
    {
        public string type { get; set; }
        public double[][][][] coordinates { get; set; }
    }
    public class InputObj
    {
        [BsonId]
        public int Id { get; set; }
        public int branch_id { get; set; }
        public GeoMultipoly Geo { get; set; }
        public GeoPoly GeoPoly { get; set; }
    }
}
