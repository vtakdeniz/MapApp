using System;
using Microsoft.AspNetCore.Mvc;
using mapService.Services;
using mapService.Models;
namespace mapService.Controllers
{
    [ApiController]
    [Route("[controller]")] 
    public class BranchController : ControllerBase
    {
        private readonly IBranchServices _branchServices;

        public BranchController(IBranchServices branchServices)
        {
            _branchServices = branchServices;
        }           

        [HttpGet]
        public IActionResult GetBranchs() {
            return Ok(_branchServices.GetBranchs());
        }
        [HttpGet("{id}", Name = "GetBranch")]
        public IActionResult GetBranch(int id)
        {
            return Ok(_branchServices.GetBranch(id));
        }

        [HttpPost]
        public IActionResult AddBranch(Branch branch) {
            _branchServices.AddBranch(branch);
            return CreatedAtRoute("GetBranch",new {id=branch.branch_id},branch);
        }
    }
}
