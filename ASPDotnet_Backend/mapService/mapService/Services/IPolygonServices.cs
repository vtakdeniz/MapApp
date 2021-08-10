using System;
using mapService.Models;
using System.Collections.Generic;

namespace mapService.Services
{
    public interface IPolygonServices
    {
        List<InputObj> GetPolygons();
        InputObj AddPolygon(InputObj inputObj);
        InputObj GetPolygon(string id);
        void DeletePolygon(string id);
        InputObj UpdatePolygon(InputObj inputObj);
    }
}
