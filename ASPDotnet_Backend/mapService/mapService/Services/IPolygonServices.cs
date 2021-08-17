using System;
using mapService.Models;
using System.Collections.Generic;
using MongoDB.Bson;
using System.Threading.Tasks;

namespace mapService.Services
{
    public interface IPolygonServices
    {
        List<BsonDocument> GetPolygons();
        BranchPolygonDto AddPolygon(BranchPolygonDto inputObj);
        BsonDocument GetPolygon(int id);
        void DeletePolygon(string id);
        List<BsonDocument> getSelectedPolygons(List<int> idList);
        //BsonDocument UpdatePolygon(PolygonDto inputObj);*/
    }
}
