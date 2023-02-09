using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProgramsNetCore.IService
{
    public interface IApprovalTaskService:IBaseService<approvaltask>
    {
        Task<List<approvaltask>> GetApprovalTask();

    }
}
