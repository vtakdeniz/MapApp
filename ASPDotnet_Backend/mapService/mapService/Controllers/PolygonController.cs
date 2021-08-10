using System;
using Microsoft.AspNetCore.Mvc;
using mapService.Services;
using mapService.Models;

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
            return Ok(_polygonServices.GetPolygons());
        }

        [HttpGet("{id}", Name = "GetPolygon")]
        public IActionResult GetPolygon(string id) {
            return Ok(_polygonServices.GetPolygon(id));
        }

        [HttpPost]
        public IActionResult AddPolygon(InputObj inputObj) {
            _polygonServices.AddPolygon(inputObj);
            return CreatedAtRoute("GetPolygon", new { Id=inputObj.Id},inputObj);
        }
    }
}
