using ProgramsNetCore.Models.Dto.UserManageDto;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProgramsNetCore.IReposity
{
    public interface IUserRoleRep
    {
        //用户人员管理列表
        Task<List<GetUserManageDto>> GetUserManagePageList(int? userId, int? departId, int pageSize, int pageIndex, RefAsync<int> total);

        //
        Task<GetUserDto> GetUserById(int id);

        //获取用户角色拥有权限
        Task<List<string>> GetRolePermissionsList(int userId);

        //获取分组角色用户列表
        Task<List<GetUserManageDto>> GetGroupUsersList(int groupId, int roleId, int pageSize, int pageIndex, RefAsync<int> total);
        Task<List<GetUserManageDto>> GetGroupUsersList(int roleId, int pageSize, int pageIndex, RefAsync<int> total);

    }
}
