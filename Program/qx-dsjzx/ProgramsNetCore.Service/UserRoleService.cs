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
    public class UserRoleService : BaseService<cloud_user_role>, IUserRoleService
    {
        private readonly IBaseRep<cloud_user_role> _dal;
        private readonly IUserRoleRep _userRoleRep;

        public UserRoleService(IBaseRep<cloud_user_role> dal, IUserRoleRep userRoleRep)
        {
            _userRoleRep=userRoleRep;
            _dal =dal;
            base._dal=dal;
        }

        public Task<List<GetUserManageDto>> GetGroupUsersList(int groupId, int roleId, int pageSize, int pageIndex, RefAsync<int> total)
        {
            return _userRoleRep.GetGroupUsersList(groupId, roleId, pageSize, pageIndex, total);
        }

        public Task<List<GetUserManageDto>> GetGroupUsersList(int roleId, int pageSize, int pageIndex, RefAsync<int> total)
        {
            return _userRoleRep.GetGroupUsersList(roleId, pageSize, pageIndex, total);
        }

        public Task<List<string>> GetRolePermissionsList(int userId)
        {
            return _userRoleRep.GetRolePermissionsList(userId);
        }

        public Task<GetUserDto> GetUserById(int id)
        {
            return _userRoleRep.GetUserById(id);
        }

        public Task<List<GetUserManageDto>> GetUserManagePageList(int? userId, int? departId, int pageSize, int pageIndex, RefAsync<int> total)
        {
            return _userRoleRep.GetUserManagePageList(userId, departId, pageSize, pageIndex, total);
        }
    }
}
