using Models;
using ProgramsNetCore.IReposity;
using ProgramsNetCore.Models;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProgramsNetCore.Reposity
{
    public class ApprovalTaskRep : IApprovalTaskRep
    {
        public SqlSugarScope db = BaseDB.Db;

        public async Task<List<approvaltask>> GetApprovalTask()
        {
            return await db.Queryable<approvaltask>().ToListAsync();
        }
    }
}
