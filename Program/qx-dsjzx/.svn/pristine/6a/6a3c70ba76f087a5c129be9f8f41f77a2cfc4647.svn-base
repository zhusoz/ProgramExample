using Models;
using ProgramsNetCore.IReposity;
using ProgramsNetCore.Models;
using ProgramsNetCore.Models.Dto.DataCollectDto;
using ProgramsNetCore.Models.Dto.DataSecurity;
using ProgramsNetCore.Models.Dto.UserManageDto;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramsNetCore.Reposity
{
    public class RoleRep : BaseRep<cloud_role>, IRoleRep
    {
        public SqlSugarScope db = BaseDB.Db;

        public Task<List<RoleGroupDto>> GetGroupRolesList(int pageSize, int pageIndex, RefAsync<int> total)
        {
            return Task.Run(() =>
            {
                return db.Queryable<cloud_role_group>()
                .Includes(group => group.Roles).ToPageListAsync(pageIndex, pageSize, total).Result
                .Select(group => new RoleGroupDto { GroupId=group.Id, GroupName=group.GroupName, RoleList=group.Roles.Select(role => new RoleDto { RoleId=role.Id, RoleName=role.Name }).ToList() }).ToList();

            });

            //return db.Queryable<cloud_role, cloud_role_group>((r, rg) => new JoinQueryInfos(JoinType.Left, r.GroupId==rg.Id))
            //    .GroupBy(r => r.GroupId)
            //    .Select((r, rg) =>
            //        new RoleGroupDto
            //        {
            //            GroupId = rg.Id,
            //            GroupName = rg.GroupName,
            //            RoleList =
            //                db.Queryable<cloud_role>()
            //                .Where(r => r.GroupId==rg.Id)
            //                .Includes(x => x.GroupId)
            //                .Select((r) => new RoleDto { RoleId=r.Id, RoleName=r.Name }).ToList(),

            //            //RoleList =
            //            //    db.Queryable<cloud_role, cloud_role_group>((r, rg) => new JoinQueryInfos(JoinType.Left, r.GroupId==rg.Id))
            //            //    .Where(r => r.GroupId==rg.Id)
            //            //    .Select((r) => new RoleDto { RoleId=r.Id, RoleName=r.Name }).ToList()
            //        }).ToListAsync();



        }

        public Task<List<UserRoleGroupDto>> GetGroupRoleUsersList()
        {
            return Task.Run(() =>
            {

                var groupList = db.Queryable<cloud_role_group>()
                .Includes(group => group.Roles).ToList()
                .Select(group => new UserRoleGroupDto
                {
                    Id=group.Id,
                    Name=group.GroupName,
                    Children=group.Roles.Select(role => new UserRoleGroupDto
                    {
                        Id=role.Id,
                        Name=role.Name,
                        Children=db.Queryable<cloud_user, cloud_user_role>((user, ur) => new JoinQueryInfos(JoinType.Inner, (int)user.id==ur.UserId))
                        .Where((user, ur) => ur.RoleId==role.Id)
                        .Select((user, ur) => new UserRoleGroupDto { Id=(int)user.id, Name=user.real_name })
                        .ToList()
                    }).ToList()
                }).ToList();
                return groupList;

                //var groupList = db.Queryable<cloud_role_group>()
                //.Includes(group => group.Roles).ToList()    //.ToPageListAsync(pageIndex, pageSize, total).Result
                //.Select(group => new RoleGroupDto
                //{
                //    GroupId=group.Id,
                //    GroupName=group.GroupName,
                //    RoleList=group.Roles.Select(role => new RoleDto
                //    {
                //        RoleId=role.Id,
                //        RoleName=role.Name,
                //        Users=db.Queryable<cloud_user, cloud_user_role>((user, ur) => new JoinQueryInfos(JoinType.Inner, (int)user.id==ur.UserId))
                //        .Where((user, ur) => ur.RoleId==role.Id)
                //        //.WhereClass(new cloud_user_role { RoleId=role.Id })
                //        .Select((user, ur) => new IdNameDto { Id=(int)user.id, Name=user.real_name })
                //        .IgnoreColumns("id")
                //        .ToList()
                //    }).ToList()
                //}).ToList();
                //return groupList;

            });
        }
    }
}
