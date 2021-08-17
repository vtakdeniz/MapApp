using System;
using System.Collections.Generic;
using mapService.DBConfig;
using mapService.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace mapService.Services
{
    public class HospitalServices : IHospitalServices
    {
        private readonly IMongoCollection<BsonDocument> _hospitals;
        private readonly IMongoCollection<BsonDocument> _polygons;

        public HospitalServices(IDbClient dbClient)
        {
            _hospitals = dbClient.GetHospitalDocumentCollection();
            _polygons = dbClient.GetPolygonDocumentCollection();
        }

        public List<BsonDocument> getHospitalsInPolygon(HospitalPolygonDto hospitalPolygonDto)
        {
            var qs = "";
            if (hospitalPolygonDto.GeoMultipoly != null) {
                qs = "{ Geo: { $geoIntersects: { $geometry: " + hospitalPolygonDto.GeoMultipoly.ToJson() + " } } }";
            }
            else {
                qs = "{ Geo: { $geoIntersects: { $geometry : " +hospitalPolygonDto.GeoPoly.ToJson()+ " } } }";
            }
            var res = _hospitals.Find(qs).ToList();
            return res;
            //var filter = Builders<BsonDocument>.Filter.Eq("branch_id", b);
            //Geo geo = new Geo();
            //var filter = Builders<BsonDocument>.Filter.GeoWithin("Geo",geo);
        }
    }
}
