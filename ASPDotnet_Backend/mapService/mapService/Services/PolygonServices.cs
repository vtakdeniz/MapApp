﻿using System;
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
        private readonly IMongoCollection<PolygonDto> _polygons;
        private readonly IMongoCollection<BsonDocument> _polygonDocument;
        public PolygonServices(IDbClient dbClient)
        {
            _polygons = dbClient.GetPolygonDtoCollection();
            _polygonDocument = dbClient.GetPolygonDocumentCollection();
        }

        public PolygonDto AddPolygon(PolygonDto inputObj)
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
            //return _polygonDocument.Find(new BsonDocument()).First();
            return _polygonDocument.Find(new BsonDocument()).ToList();
        }

        /*public BsonDocument UpdatePolygon(PolygonDto inputObj)
        {
            GetPolygon(inputObj.Id);
            _polygons.ReplaceOne(poly=>poly.Id==inputObj.Id,inputObj);
            return inputObj;
        }*/

        public BsonDocument castPolygon(PolygonDto polygonDto) {
            Polygon p = new Polygon();
            Geo g = new Geo();
            p.Geo = g;
            p.branch_id = polygonDto.branch_id;
            if (polygonDto.GeoMultipoly == null) {
                p.Geo.coordinates = polygonDto.GeoPoly.coordinates[0];
                g.type = polygonDto.GeoPoly.type;
                _polygons.InsertOne(polygonDto);
                p.Id = polygonDto.Id;
                DeletePolygon(polygonDto.Id);
                BsonDocument document = new BsonDocument {  BsonDocument.Parse(p.ToJson()) };
                 return document;
            }
            else
            {
                BsonDocument document = new BsonDocument { { "branch_id", polygonDto.branch_id }, { "Geo", BsonDocument.Parse(polygonDto.GeoMultipoly.ToJson()) } };
                 return document;
            }
        }
    }
}
