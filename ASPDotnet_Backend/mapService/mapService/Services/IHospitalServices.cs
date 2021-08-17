using System;
using System.Collections.Generic;
using MongoDB.Bson;

namespace mapService.Services
{
    public interface IHospitalServices
    {
        List<BsonDocument> hospitalsInPolygon();
    }
}
