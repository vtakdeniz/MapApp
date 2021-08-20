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
        private readonly IMongoCollection<BsonDocument> _hospitalPolygonDocument;
        private readonly IMongoCollection<HospitalPolygonDto> _hospitalPolygons;

        public HospitalServices(IDbClient dbClient)
        {
            _hospitals = dbClient.GetHospitalDocumentCollection();
            _hospitalPolygonDocument = dbClient.GetHospitalPolygonDocumentCollection();
            _hospitalPolygons = dbClient.GetHospitalPolygonCollection();
        }

        public List<BsonDocument> getHospitalsInPolygon(HospitalPolygonDto hospitalPolygonDto)
        {
            BsonDocument doc = castPolygon(hospitalPolygonDto);
            _hospitalPolygonDocument.InsertOne(doc);

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

        public List<BsonDocument> getHospitalsInPolygonNoSave(HospitalPolygonDto hospitalPolygonDto)
        {
            var qs = "";

            if (hospitalPolygonDto.GeoMultipoly != null)
            {
                qs = "{ Geo: { $geoIntersects: { $geometry: " + hospitalPolygonDto.GeoMultipoly.ToJson() + " } } }";
            }
            else
            {
                qs = "{ Geo: { $geoIntersects: { $geometry : " + hospitalPolygonDto.GeoPoly.ToJson() + " } } }";
            }

            var res = _hospitals.Find(qs);
            List<BsonDocument> res_to_send=new List<BsonDocument>();
            if (res.Count() > 0) {
                res_to_send=res.ToList();
            }
            return res_to_send;
        }


        public BsonDocument castPolygon(HospitalPolygonDto hospitalPolygonDto) {
            if (hospitalPolygonDto.GeoMultipoly == null)
            {
                BsonDocument document = new BsonDocument { { "polygon_name", hospitalPolygonDto.polygon_name }, { "Geo", BsonDocument.Parse(hospitalPolygonDto.GeoPoly.ToJson()) } };
                return document;
            }
            else
            {
                BsonDocument document = new BsonDocument { { "polygon_name", hospitalPolygonDto.polygon_name}, { "Geo", BsonDocument.Parse(hospitalPolygonDto.GeoMultipoly.ToJson()) } };
                return document;
            }
        }

        public List<string> getHospitalPolygonNameList()
        {
            List<string> names = new List<string>();

            foreach (HospitalPolygonDto hospital in _hospitalPolygons.Find(hospitalx=>true).ToList()) {
                names.Add(hospital.polygon_name);
            }
            return names;
            
        }

        public List<BsonDocument> getSelectedHospitalPolygons(List<string> names) {
            List<BsonDocument> hospitals = new List<BsonDocument>();

            foreach (string name in names) {
                var filter = Builders<BsonDocument>.Filter.Eq("polygon_name", name);
                var hospital = _hospitalPolygonDocument.Find(filter).FirstOrDefault();

                if (hospital!=null) {
                    hospitals.Add(hospital);
                }
            }
            return hospitals;
        }
    }
}
