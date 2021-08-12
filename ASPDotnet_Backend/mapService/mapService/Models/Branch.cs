using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace mapService.Models
{
    public class Point
    {
        public string type { get; set; }
        public double[] coordinates { get; set; }
    }

    public class Branch
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public int branch_id { get; set; }
        public string branch_manager { get; set; }
        public string branch_name { get; set; }
        //public double[] branch_crd { get; set; }
        public Point Geo { get; set; }
    }
}

