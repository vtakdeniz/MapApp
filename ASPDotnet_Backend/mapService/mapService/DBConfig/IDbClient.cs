using System;
using MongoDB.Driver;
using mapService.Models;

namespace mapService.DBConfig
{
    public interface IDbClient
    {
        IMongoCollection<Branch> GetBranchCollection();
        IMongoCollection<InputObj> GetPolygonCollection();
    }
}
