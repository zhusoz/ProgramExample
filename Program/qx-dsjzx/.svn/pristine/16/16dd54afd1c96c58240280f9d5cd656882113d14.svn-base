using Models;
using ProgramsNetCore.Models.Dto.UserManageDto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProgramsNetCore.IService
{
    public interface IRolePermissionService:IBaseService<cloud_role_permission>
    {


        Task<List<PermissionDto>> GetAllPermissionsList();


        Task<List<PermissionDto>> GetRolePermissionsList(int roleId);
    }
}
