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
    public class HospitalController : ControllerBase
    {
        private readonly IHospitalServices _hospitalServices;

        public HospitalController(IHospitalServices hospitalServices)
        {
            _hospitalServices = hospitalServices;
        }

        [HttpPost]
        public IActionResult getHospitalsInPolygon(HospitalPolygonDto hospitalPolygonDto) {

            if (hospitalPolygonDto == null)
            {
                return BadRequest();
            }

            return Ok(_hospitalServices.getHospitalsInPolygon(hospitalPolygonDto));
        }

        [HttpGet("GetPolygonList")]
        public IActionResult getHospitalPolygonNameList() {
            return Ok(_hospitalServices.getHospitalPolygonNameList());
        }

        [HttpPost("GetPolygons")]
        public IActionResult getSelectedHospitalPolygons(List<string> names) {
            return Ok(_hospitalServices.getSelectedHospitalPolygons(names));
        }

        [HttpPost("GetHospitalsNoSave")]
        public IActionResult getSelectedHospitalPolygonsNoSave(HospitalPolygonDto hospitalPolygonDto)
        {

            if (hospitalPolygonDto == null)
            {
                return BadRequest();
            }

            return Ok(_hospitalServices.getHospitalsInPolygonNoSave(hospitalPolygonDto));
        }
    }
}
