using Models;
using ProgramsNetCore.IReposity;
using ProgramsNetCore.Models;
using ProgramsNetCore.Models.Dto.UserManageDto;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramsNetCore.Reposity
{
    public class RolePermissionRep : BaseRep<cloud_role_permission>, IRolePermissionRep
    {
        public SqlSugarScope db = BaseDB.Db;

        public Task<List<PermissionDto>> GetAllPermissionsList()
        {
            return Task.Run(() =>
            {
                //主目录列表
                var mainlist1 = db.Queryable<cloud_permission>()
                    .Where(p => p.ParentId==null)
                    .Select(p => new PermissionDto { Id=p.Id, Permission=p.Permission, Url=p.Url })
                    .ToList();

                foreach (var item in mainlist1)
                {
                    var list = db.Queryable<cloud_permission>()
                        .Includes(p => p.Parent)
                        .ToTree(p => p.Child, p => p.ParentId, item.Id);

                    item.PermissionList=list.Select(e => new PermissionDto { Id=e.Id, Permission=e.Permission, Url=e.Url }).ToList();
                }

                return mainlist1;
            });


        }

        public Task<List<PermissionDto>> GetRolePermissionsList(int roleId)
        {
            return db.Queryable<cloud_role_permission, cloud_permission>((rolePermission, permission) => new JoinQueryInfos(JoinType.Inner, rolePermission.PermissionId==permission.Id))
                .Where(rolePermission => rolePermission.RoleId==roleId)
                .Select((rolePermission, permission) => new PermissionDto { Id=permission.Id, Permission=permission.Permission, Url=permission.Url })
                .ToListAsync();


        }
    }
}
