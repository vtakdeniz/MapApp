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

    public class Geo
    {
        public string type { get; set; }
        public double[][][] coordinates { get; set; }
    }


    public class PolygonDto
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public int branch_id { get; set; }

        //[BsonIgnoreIfNull]
        public GeoMultipoly GeoMultipoly { get; set; }
        
        //[BsonIgnoreIfNull]
        public GeoPoly GeoPoly { get; set; }
 
    }

    public class Polygon    
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public int branch_id { get; set; }
        public Geo Geo { get; set; }
    }
}

