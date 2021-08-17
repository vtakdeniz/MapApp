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
    }
}
