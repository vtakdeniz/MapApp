using System;
using System.Collections.Generic;
using mapService.Models;
using MongoDB.Driver;
using mapService.DBConfig;

namespace mapService.Services
{
    public class BranchServices : IBranchServices
    {
        private readonly IMongoCollection<Branch> _branchs;

        public BranchServices(IDbClient dbClient)
        {
            _branchs = dbClient.GetBranchCollection();
        }

        public Branch AddBranch(Branch branch)
        {
            _branchs.InsertOne(branch);
            return branch;
        }

        public void DeleteBranch(int id)
        {
            _branchs.DeleteOne(branch => branch.branch_id==id);
        }

        public Branch GetBranch(int id)
        {
            return _branchs.Find(branch => branch.branch_id == id).First();
        }

        public List<Branch> GetBranchs()
        {
            return _branchs.Find(branch => true).ToList();
        }

        public Branch UpdateBranch(Branch branchx)
        {
            GetBranch(branchx.branch_id);
            _branchs.ReplaceOne(branch=>branch.branch_id==branchx.branch_id,branchx);
            return branchx;
        }
    }
}
