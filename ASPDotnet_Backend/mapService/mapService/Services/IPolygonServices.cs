﻿using System;
using mapService.Models;
using System.Collections.Generic;
using MongoDB.Bson;
using System.Threading.Tasks;

namespace mapService.Services
{
    public interface IPolygonServices
    {
        List<BsonDocument> GetPolygons();
        PolygonDto AddPolygon(PolygonDto inputObj);
        BsonDocument GetPolygon(int id);
        void DeletePolygon(string id);
        //BsonDocument UpdatePolygon(PolygonDto inputObj);*/
    }
}
