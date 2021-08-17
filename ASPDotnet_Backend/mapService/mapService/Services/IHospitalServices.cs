using System;
using System.Collections.Generic;
using MongoDB.Bson;
using mapService.Models;
namespace mapService.Services
{
    public interface IHospitalServices
    {
        List<BsonDocument> getHospitalsInPolygon(HospitalPolygonDto hospitalPolygonDto);
    }
}
