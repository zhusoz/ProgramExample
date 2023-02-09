using Models;
using ProgramsNetCore.Models.Dto.DataSecurity;
using ProgramsNetCore.Models.Dto.UserManageDto;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProgramsNetCore.IReposity
{
    public interface IRoleRep
    {
        
        Task<List<RoleGroupDto>> GetGroupRolesList(int pageSize, int pageIndex, RefAsync<int> total);


        Task<List<UserRoleGroupDto>> GetGroupRoleUsersList();
    }
}
