using Models;
using ProgramsNetCore.IReposity;
using ProgramsNetCore.IService;
using ProgramsNetCore.Models.Dto.UserManageDto;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProgramsNetCore.Service
{
    public class RoleService : BaseService<cloud_role>, IRoleService
    {
        private readonly IBaseRep<cloud_role> _dal;
        private readonly IRoleRep _roleRep;

        public RoleService(IBaseRep<cloud_role> dal,IRoleRep roleRep)
        {
            _dal=dal;
            _roleRep=roleRep;
            base._dal=dal;
        }

        public Task<List<RoleGroupDto>> GetGroupRolesList(int pageSize, int pageIndex, RefAsync<int> total)
        {
            return _roleRep.GetGroupRolesList(pageSize,pageIndex,total);
        }

        public Task<List<UserRoleGroupDto>> GetGroupRoleUsersList()
        {
            return _roleRep.GetGroupRoleUsersList();
        }
    }
}
