using System;
using mapService.Models;
using System.Collections.Generic;
using MongoDB.Driver;
using mapService.DBConfig;
using MongoDB.Bson;
using System.Threading.Tasks;
namespace mapService.Services
{
    public class PolygonServices : IPolygonServices
    {
        private readonly IMongoCollection<BranchPolygonDto> _polygons;
        private readonly IMongoCollection<BsonDocument> _polygonDocument;

        public PolygonServices(IDbClient dbClient)
        {
            _polygons = dbClient.GetPolygonDtoCollection();
            _polygonDocument = dbClient.GetPolygonDocumentCollection();
        }

        public BranchPolygonDto AddPolygon(BranchPolygonDto inputObj)
        {

            //TODO: gelen input obj bir son dökümana çevirip kaydet - Dökümanı el ile inşa et
            BsonDocument document = castPolygon(inputObj);
            _polygonDocument.InsertOne(document);
            return inputObj;
        }

        public void DeletePolygon(string id)
        {
            _polygons.DeleteOne(poly => poly.Id==id);
        }

        public BsonDocument GetPolygon(int id)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("branch_id", id);
            return _polygonDocument.Find(filter).FirstOrDefault();
        }
        
        public List<BsonDocument> GetPolygons()
        {
            return _polygonDocument.Find(new BsonDocument()).ToList();
        }

        /*public BsonDocument UpdatePolygon(PolygonDto inputObj)
        {
            GetPolygon(inputObj.Id);
            _polygons.ReplaceOne(poly=>poly.Id==inputObj.Id,inputObj);
            return inputObj;
        }*/

        public BsonDocument castPolygon(BranchPolygonDto polygonDto) {
            //GeoPoly g = new GeoPoly();
            if (polygonDto.GeoMultipoly == null) {
                //g.coordinates = polygonDto.GeoPoly.coordinates[0];
                //g.type = polygonDto.GeoPoly.type;
                BsonDocument document = new BsonDocument { { "branch_id", polygonDto.branch_id }, { "Geo", BsonDocument.Parse(polygonDto.GeoPoly.ToJson()) } };
                 return document;
            }
            else
            {
                BsonDocument document = new BsonDocument { { "branch_id", polygonDto.branch_id }, { "Geo", BsonDocument.Parse(polygonDto.GeoMultipoly.ToJson()) } };
                 return document;
            }
        }

        public List<BsonDocument> getSelectedPolygons(List<int> idList) {
            List<BsonDocument> postData = new List<BsonDocument>();

            foreach (int id in idList) {
                var temp = GetPolygon(id);
                if (temp!=null) {
                    postData.Add(temp);
                }
            }
            return postData;
        }

    }
}
