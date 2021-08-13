using System;
using mapService.Services;
using mapService.Models;
using System.Collections.Generic;

namespace mapService.Services
{
    public interface IBranchServices
    {
        List<Branch> GetBranchs();
        Branch AddBranch(Branch branch);
        Branch GetBranch(int id);
        void DeleteBranch(int id);
        Branch UpdateBranch(Branch branch);
        List<int> getBranchIdList();
    }
}
