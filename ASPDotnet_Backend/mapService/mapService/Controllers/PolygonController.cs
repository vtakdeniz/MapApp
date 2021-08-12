using System;
using Microsoft.AspNetCore.Mvc;
using mapService.Services;
using mapService.Models;
using System.Collections.Generic;
using MongoDB.Bson;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace mapService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PolygonController : ControllerBase
    {
        private readonly IPolygonServices _polygonServices;

        public PolygonController(IPolygonServices polygonServices)
        {
            _polygonServices = polygonServices;
        }

        [HttpGet]
        public IActionResult GetPolygons() {
            List<BsonDocument> docs = _polygonServices.GetPolygons();
            //BsonDocument docs = _polygonServices.GetPolygons();
            //HttpContext.Response.Headers["Content-Type"] = "application/json;charset=utf-8";
            return Ok(docs);
        }

        [HttpGet("{id}", Name = "GetPolygon")]
        public IActionResult GetPolygon(int id) {
            return Ok(_polygonServices.GetPolygon(id));
        }

        [HttpPost]
        public IActionResult AddPolygon(PolygonDto inputObj) {
            
            _polygonServices.AddPolygon(inputObj);
            return Ok();
        }
    }
}
