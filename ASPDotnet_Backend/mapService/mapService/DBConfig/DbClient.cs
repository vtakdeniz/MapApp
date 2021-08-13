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
        private readonly IMongoCollection<PolygonDto> _polygonDto;
        private readonly IMongoCollection<BsonDocument> _polygonDocument;

        public DbClient(IOptions<mapServiceDatabaseSettings> mapServiceDbConfig)
        {
            var dbclient = new MongoClient(mapServiceDbConfig.Value.ConnectionString);
            var database = dbclient.GetDatabase(mapServiceDbConfig.Value.DatabaseName);
            _branches = database.GetCollection<Branch>(mapServiceDbConfig.Value.BranchCollectionName);
            _polygonDto = database.GetCollection<PolygonDto>(mapServiceDbConfig.Value.PolygonCollectionName);
            _polygonDocument = database.GetCollection<BsonDocument>(mapServiceDbConfig.Value.PolygonCollectionName);
        }

        public IMongoCollection<Branch> GetBranchCollection() {
            return _branches;
        }
        public IMongoCollection<PolygonDto> GetPolygonDtoCollection()
        {
            return _polygonDto;
        }
        public IMongoCollection<BsonDocument> GetPolygonDocumentCollection()
        {
            return _polygonDocument;
        }
    }
}
