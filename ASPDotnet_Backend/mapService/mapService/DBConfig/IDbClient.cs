using System;
using MongoDB.Driver;
using mapService.Models;
using MongoDB.Bson;

namespace mapService.DBConfig
{
    public interface IDbClient
    {
        IMongoCollection<Branch> GetBranchCollection();
        IMongoCollection<BranchPolygonDto> GetPolygonDtoCollection();
        //IMongoCollection<Polygon> GetPolygonCollection();
        IMongoCollection<BsonDocument> GetPolygonDocumentCollection();
        IMongoCollection<BsonDocument> GetHospitalDocumentCollection();
        //IMongoCollection<BsonDocument> GetHospitalCollection();
        IMongoCollection<BsonDocument> GetHospitalPolygonDocumentCollection();
    }
}
