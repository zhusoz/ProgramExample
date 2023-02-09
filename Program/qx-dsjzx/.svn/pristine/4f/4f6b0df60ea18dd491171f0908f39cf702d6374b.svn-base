using Models;
using ProgramsNetCore.Models.Dto.UserManageDto;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProgramsNetCore.IService
{
    public interface IRoleService:IBaseService<cloud_role>
    {

        //获取分组
        Task<List<RoleGroupDto>> GetGroupRolesList(int pageSize, int pageIndex, RefAsync<int> total);

        //获取分组及用户
        Task<List<UserRoleGroupDto>> GetGroupRoleUsersList();
    }
}
