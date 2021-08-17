using System;
using System.Collections.Generic;
using mapService.DBConfig;
using MongoDB.Bson;
using MongoDB.Driver;

namespace mapService.Services
{
    public class HospitalServices : IHospitalServices
    {
        private readonly IMongoCollection<BsonDocument> _hospitals;

        public HospitalServices(IDbClient dbClient)
        {
            _hospitals = dbClient.GetHospitalDocumentCollection();
        }

        public List<BsonDocument> hospitalsInPolygon(BsonDocument b)
        {
            //var filter = Builders<BsonDocument>.Filter.Eq("branch_id", b);
            var filter = Builders<BsonDocument>.Filter.GeoWithin("Geo",b);
            throw new NotImplementedException();
        }
    }
}
