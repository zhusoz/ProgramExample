using Models;
using ProgramsNetCore.IReposity;
using ProgramsNetCore.IService;
using ProgramsNetCore.Models.Dto.UserManageDto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProgramsNetCore.Service
{
    public class RolePermissionService : BaseService<cloud_role_permission>, IRolePermissionService
    {
        private readonly IBaseRep<cloud_role_permission> _dal;
        private readonly IRolePermissionRep _rep;

        public RolePermissionService(IBaseRep<cloud_role_permission> dal,IRolePermissionRep rep)
        {
            _dal = dal;
            _rep=rep;
            base._dal = dal;
        }

        public Task<List<PermissionDto>> GetAllPermissionsList()
        {
            return _rep.GetAllPermissionsList();
        }

        public Task<List<PermissionDto>> GetRolePermissionsList(int roleId)
        {
            return _rep.GetRolePermissionsList(roleId);
        }
    }
}
