using Models;
using ProgramsNetCore.IReposity;
using ProgramsNetCore.Models;
using ProgramsNetCore.Models.Dto.UserManageDto;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProgramsNetCore.Reposity
{
    public class UserRoleRep : BaseRep<cloud_user_role>, IUserRoleRep
    {
        public SqlSugarScope db = BaseDB.Db;

        public Task<List<GetUserManageDto>> GetGroupUsersList(int groupId, int roleId, int pageSize, int pageIndex, RefAsync<int> total)
        {

            return db.Queryable<cloud_role, cloud_user_role, cloud_user, cloud_org>((r, ur, u, org) => new JoinQueryInfos(JoinType.Left, r.Id==ur.RoleId, JoinType.Left, ur.UserId==(int)u.id, JoinType.Left, u.description==org.Id))
                .WhereClass(new cloud_user_role { RoleId=roleId }, true)
                .Where(r => r.GroupId==groupId)
                .OrderBy(((r, ur, u, org) => u.id))
                .Select((r, ur, u, org) => new GetUserManageDto { Id = (int)u.id, Department = org.Title, IDCard = u.id_card, Name = u.real_name, Phone = u.phone, RoleName = r.Name, Account = u.account, RoleId = r.Id, Password = u.password, DepartmentId = org.Id })
                .ToPageListAsync(pageIndex, pageSize, total);

        }

        public Task<List<GetUserManageDto>> GetGroupUsersList(int roleId, int pageSize, int pageIndex, RefAsync<int> total)
        {
            return db.Queryable<cloud_role, cloud_user_role, cloud_user, cloud_org>((r, ur, u, org) => new JoinQueryInfos(JoinType.Left, r.Id==ur.RoleId, JoinType.Left, ur.UserId==(int)u.id, JoinType.Left, u.description==org.Id))
                .WhereClass(new cloud_user_role { Id = roleId }, true)
                .OrderBy(((r, ur, u, org) => u.id))
                .Select((r, ur, u, org) => new GetUserManageDto { Id=(int)u.id, Department=org.Title, IDCard=u.id_card, Name=u.real_name, Phone=u.phone, RoleName=r.Name, Account=u.account, RoleId=r.Id, Password=u.password, DepartmentId=org.Id })
                .ToPageListAsync(pageIndex, pageSize, total);
        }

        public Task<List<string>> GetRolePermissionsList(int userId)
        {
            return db.Queryable<cloud_user_role, cloud_role_permission, cloud_permission, cloud_role>((ur, rp, p, r) => new JoinQueryInfos(JoinType.Left, ur.RoleId==rp.RoleId, JoinType.Left, rp.PermissionId==p.Id, JoinType.Left, ur.RoleId==r.Id))
                .Where(ur => ur.UserId==userId)
                .Select((ur, rp, p, r) => p.Permission)
                .ToListAsync();
        }

        public Task<GetUserDto> GetUserById(int id)
        {
            return db.Queryable<cloud_user, cloud_user_role, cloud_role, cloud_org>((u, ur, r, org) => new JoinQueryInfos(JoinType.Left, ur.UserId==(int)u.id, JoinType.Left, ur.RoleId==r.Id, JoinType.Left, u.description==org.Id))
                    .Where(u => u.is_deleted==0)
                    .Select((u, ur, r, org) => new GetUserDto { Id= (int)u.id, Department=org.Title, IDCard=u.id_card, Name=u.real_name, Phone=u.phone, RoleName=r.Name, Sex=u.sex })
                    .FirstAsync(u => u.Id==id);

        }

        public Task<List<GetUserManageDto>> GetUserManagePageList(int? roleId, int? departId, int pageSize, int pageIndex, RefAsync<int> total)
        {

            return db.Queryable<cloud_user, cloud_user_role, cloud_role, cloud_org>((u, ur, r, org) => new JoinQueryInfos(JoinType.Left, ur.UserId==(int)u.id, JoinType.Left, ur.RoleId==r.Id, JoinType.Left, u.description==org.Id))
                    .Where(u => u.is_deleted==0).WhereIF(roleId.HasValue, (u, ur, r, org) => ur.RoleId==roleId)
                    .WhereIF(departId.HasValue, (u, ur, r, org) => u.description==departId)
                    .Select((u, ur, r, org) => new GetUserManageDto { Id = (int)u.id, Department = org.Title, IDCard = u.id_card, Name = u.real_name, Phone = u.phone, RoleName = r.Name, Account = u.account, RoleId = r.Id, Password = u.password, DepartmentId = org.Id })
                    .ToPageListAsync(pageIndex, pageSize, total);

        }


    }
}
