using System;
using mapService.Models;
using System.Collections.Generic;
using MongoDB.Driver;
using mapService.DBConfig;

namespace mapService.Services
{
    public class PolygonServices : IPolygonServices
    {
        private readonly IMongoCollection<InputObj> _polygons;

        public PolygonServices(IDbClient dbClient)
        {
            _polygons = dbClient.GetPolygonCollection();
        }

        public InputObj AddPolygon(InputObj inputObj)
        {
            _polygons.InsertOne(inputObj);
            return inputObj;
        }

        public void DeletePolygon(string id)
        {
            _polygons.DeleteOne(poly => poly.Id==id);
        }

        public InputObj GetPolygon(string id)
        {
           return _polygons.Find(poly => poly.Id == id).First();
        }

        public List<InputObj> GetPolygons()
        {
            return _polygons.Find(poly => true).ToList();
        }

        public InputObj UpdatePolygon(InputObj inputObj)
        {
            GetPolygon(inputObj.Id);
            _polygons.ReplaceOne(poly=>poly.Id==inputObj.Id,inputObj);
            return inputObj;
        }
    }
}
