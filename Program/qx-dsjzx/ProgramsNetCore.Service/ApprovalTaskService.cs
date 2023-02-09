using Models;
using ProgramsNetCore.IReposity;
using ProgramsNetCore.IService;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProgramsNetCore.Service
{
    public class ApprovalTaskService : BaseService<approvaltask>, IApprovalTaskService
    {
        private readonly IBaseRep<approvaltask> _dal;
        private readonly IApprovalTaskRep _approvalTaskRep;

        public ApprovalTaskService(IBaseRep<approvaltask> dal,IApprovalTaskRep approvalTaskRep)
        {
            _dal = dal;
            base._dal=dal;
            _approvalTaskRep=approvalTaskRep;
        }

        

        public async Task<List<approvaltask>> GetApprovalTask()
        {
            return await _approvalTaskRep.GetApprovalTask();
        }
    }
}
