using System;
using MongoDB.Driver;
using mapService.Models;
using Microsoft.Extensions.Options;

namespace mapService.DBConfig
{
    public class DbClient :IDbClient
    {
        private readonly IMongoCollection<Branch> _branches;
        private readonly IMongoCollection<InputObj> _polygons;

        public DbClient(IOptions<mapServiceDatabaseSettings> mapServiceDbConfig)
        {
            var dbclient = new MongoClient(mapServiceDbConfig.Value.ConnectionString);
            var database = dbclient.GetDatabase(mapServiceDbConfig.Value.DatabaseName);
            _branches = database.GetCollection<Branch>(mapServiceDbConfig.Value.BranchCollectionName);
            _polygons = database.GetCollection<InputObj>(mapServiceDbConfig.Value.PolygonCollectionName);
        }

        public IMongoCollection<Branch> GetBranchCollection() {
            return _branches;
        }
        public IMongoCollection<InputObj> GetPolygonCollection()
        {
            return _polygons;
        }
    }
}
