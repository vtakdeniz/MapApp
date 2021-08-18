using System;
using MongoDB.Driver;
using mapService.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;

namespace mapService.DBConfig
{
    public class DbClient :IDbClient
    {
        private readonly IMongoCollection<Branch> _branches;
        private readonly IMongoCollection<BranchPolygonDto> _polygonDto;
        private readonly IMongoCollection<BsonDocument> _polygonDocument;
        private readonly IMongoCollection<BsonDocument> _hospitalDocument;
        private readonly IMongoCollection<BsonDocument> _hospitalPolygonDocument;
        private readonly IMongoCollection<HospitalPolygonDto> _hospitalPolygon;

        public DbClient(IOptions<mapServiceDatabaseSettings> mapServiceDbConfig)
        {
            var dbclient = new MongoClient(mapServiceDbConfig.Value.ConnectionString);
            var database = dbclient.GetDatabase(mapServiceDbConfig.Value.DatabaseName);
            _branches = database.GetCollection<Branch>(mapServiceDbConfig.Value.BranchCollectionName);
            _polygonDto = database.GetCollection<BranchPolygonDto>(mapServiceDbConfig.Value.PolygonCollectionName);
            _polygonDocument = database.GetCollection<BsonDocument>(mapServiceDbConfig.Value.PolygonCollectionName);
            _hospitalDocument = database.GetCollection<BsonDocument>(mapServiceDbConfig.Value.HospitalCollectionName);
            _hospitalPolygonDocument = database.GetCollection<BsonDocument>(mapServiceDbConfig.Value.HospitalPolygonCollectionName);
            _hospitalPolygon = database.GetCollection<HospitalPolygonDto>(mapServiceDbConfig.Value.HospitalPolygonCollectionName);
        }

        public IMongoCollection<Branch> GetBranchCollection() {
            return _branches;
        }
        public IMongoCollection<BranchPolygonDto> GetPolygonDtoCollection()
        {
            return _polygonDto;
        }
        public IMongoCollection<BsonDocument> GetPolygonDocumentCollection()
        {
            return _polygonDocument;
        }

        public IMongoCollection<BsonDocument> GetHospitalDocumentCollection()
        {
            return _hospitalDocument;
        }

        public IMongoCollection<BsonDocument> GetHospitalPolygonDocumentCollection()
        {
            return _hospitalPolygonDocument;
        }

        public IMongoCollection<HospitalPolygonDto> GetHospitalPolygonCollection()
        {
            return _hospitalPolygon;
        }
    }
}
