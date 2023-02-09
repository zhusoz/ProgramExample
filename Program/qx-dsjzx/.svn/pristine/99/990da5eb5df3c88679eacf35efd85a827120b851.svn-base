using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using ProblemDisposal.Common.Basic;
using ProgramsNetCore.Common;
using ProgramsNetCore.Common.Basic;
using ProgramsNetCore.IService;
using ProgramsNetCore.Models.Dto.UserManageDto;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataSharing.Controllers
{
    [Route("")]
    [ApiController]
    public class UserManageController : ControllerBase
    {
        private readonly IDataTableService _dataTableService;
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IPermissionService _permissionService;
        private readonly IUserRoleService _userRoleService;
        private readonly IRolePermissionService _rolePermissionService;
        private readonly IAttributionService _attributionService;
        private readonly IOrgService _orgService;
        private readonly IGroupService _groupService;

        public UserManageController(IDataTableService dataTableService, IUserService userService, IRoleService roleService, IPermissionService permissionService, IUserRoleService userRoleService, IRolePermissionService rolePermissionService, IAttributionService attributionService, IOrgService orgService, IGroupService groupService)
        {
            _dataTableService=dataTableService;
            _userService=userService;
            _roleService=roleService;
            _permissionService=permissionService;
            _userRoleService=userRoleService;
            _rolePermissionService=rolePermissionService;
            _attributionService=attributionService;
            _orgService=orgService;
            _groupService=groupService;
        }


        /// <summary>
        /// 获取部门列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<AjaxResult<List<GetDepartsDto>>> GetDepartsList()
        {
            string sql = "SELECT `Id`,`Title` FROM `data_sharing_platform`.`cloud_org`;";
            var result = await _dataTableService.GetEntityList<GetDepartsDto>(sql);

            //await Logger.AddPlatformLog("获取部门列表", LogType.DataAccess);

            return new AjaxResult<List<GetDepartsDto>>(result);
        }

        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<AjaxResult<List<GetRolesDto>>> GetRolesList()
        {
            string sql = "SELECT `Id`,`Name` RoleName FROM `data_sharing_platform`.`cloud_role`;";
            var result = await _dataTableService.GetEntityList<GetRolesDto>(sql);

            //await Logger.AddPlatformLog("获取角色列表", LogType.DataAccess);

            return new AjaxResult<List<GetRolesDto>>(result);
        }

        /// <summary>
        /// 人员管理
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PageResult<List<GetUserManageDto>>> GetUserManageList(GetUserManageListDto dto)
        {
            if (dto.PageIndex==0) dto.PageIndex=1;
            if (dto.PageSize==0) dto.PageSize=10;

            RefAsync<int> total = 0;
            var list = await _userRoleService.GetUserManagePageList(dto.RoleId, dto.DepartId, dto.PageSize, dto.PageIndex, total);

            //await Logger.AddPlatformLog("获取人员管理列表", LogType.DataAccess);
            
            return new PageResult<List<GetUserManageDto>>(list, total, total%dto.PageSize==0 ? total/dto.PageSize : total/dto.PageSize+1, dto.PageSize, dto.PageIndex);
        }

        /// <summary>
        /// 查询id用户
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<AjaxResult<GetUserDto>> GetUser(int id)
        {
            var entity = await _userRoleService.GetUserById(id);

            //await Logger.AddPlatformLog("查询用户", LogType.DataAccess);

            return new AjaxResult<GetUserDto>(entity);
        }

        /// <summary>
        /// 新增人员
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<bool>> AddUser(AddUserDto dto)
        {
            if (dto.Sex!=0 && dto.Sex!=1) return new AjaxResult<bool>(false, "性别填写错误");

            //1.account去重
            var entity = await _userService.GetEntity(e => e.account==dto.Account);
            if (entity!=null) return new AjaxResult<bool>(false, "此账号已被他人占用");

            //判断部门id 是否合法
            var isDepartIdExist = await _orgService.GetEntity(e => e.Id==dto.DepartId);
            if (isDepartIdExist==null) return new AjaxResult<bool>(false, "部门Id错误");
            //判断roleId 是否合法
            var isRoleIdExistEntity = await _roleService.GetEntity(e => e.Id==dto.RoleId);
            if (isRoleIdExistEntity==null) return new AjaxResult<bool>(false, "角色Id错误");


            //2.add
            var result = await _userService.IdAdd(new cloud_user
            {
                account=dto.Account,
                real_name=dto.RealName,
                password=dto.Password,
                address=dto.Address,
                sex=dto.Sex,
                gmt_create=DateTime.Now,
                id_card=dto.IDCard,
                phone=dto.Phone,
                is_deleted=0,
                is_enabled=0,
                is_locked=0,
                description=dto.DepartId
            });
            string sql = $" insert into cloud_user_role(roleid,userid,type) values({dto.RoleId},{result},1) ";
            await _dataTableService.NoQueryExcuteSql(sql);
            //await Logger.AddPlatformLog("新增人员", LogType.DataOperation);

            return new AjaxResult<bool>(result>0);
        }

        /// <summary>
        /// 编辑人员
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<bool>> UserEdit(EditUserDto dto)
        {
            if (dto.Sex!=0 || dto.Sex!=1) return new AjaxResult<bool>(false, "性别填写错误");

            //1.账号冲突
            var accountEntity = await _userService.GetEntity(e => e.account==dto.Account);
            if (accountEntity != null) return new AjaxResult<bool>(false, "此账号已被他人占用");

            //判断部门id 是否合法
            var isDepartIdExistEntity = await _orgService.GetEntity(e => e.Id==dto.DepartId);
            if (isDepartIdExistEntity == null) return new AjaxResult<bool>(false, "部门Id错误");
            //判断roleId 是否合法
            var isRoleIdExistEntity = await _roleService.GetEntity(e => e.Id==dto.RoleId);
            if (isRoleIdExistEntity==null) return new AjaxResult<bool>(false, "角色Id错误");


            //2.拿到实体
            var entity = await _userService.GetEntity(e => (int)e.id==dto.Id);
            if (entity==null) return new AjaxResult<bool>(false, "Id参数错误");
            entity.password = dto.Password;
            entity.address = dto.Address;
            entity.sex = dto.Sex;
            entity.account = dto.Account;
            entity.real_name = dto.RealName;
            entity.phone = dto.Phone;
            entity.id_card=dto.IDCard;
            entity.description = dto.DepartId;
            await _userService.Update(entity);
            await _userRoleService.UpdateColumn(new cloud_user_role { RoleId=dto.RoleId }, e => e.UserId);

            //await Logger.AddPlatformLog("编辑人员", LogType.DataOperation);

            //3.
            return new AjaxResult<bool>(true);

        }


        /// <summary>
        /// 密码重置
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<AjaxResult<bool>> PasswordReset(int id)
        {
            var user = await _userService.GetEntity(e => (int)e.id==id);
            if (user==null) return new AjaxResult<bool>(false, "该用户不存在");

            user.password = "WEZbv1YkRvwOshBbZfWbgw==";//重置密码为null
            var result = await _userService.Update(user);

            //await Logger.AddPlatformLog("密码重置", LogType.DataOperation);

            return new AjaxResult<bool>(result);
        }


        /// <summary>
        /// 删除人员
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<bool>> DeleteUser(int id)
        {
            var user = await _userService.GetEntity(e => (int)e.id==id);
            if (user==null) return new AjaxResult<bool>(false, "该用户不存在");

            user.is_deleted=1;
            var result = await _userService.Update(user);

            //await Logger.AddPlatformLog("删除人员", LogType.DataOperation);

            return new AjaxResult<bool>(result);
        }


        /// <summary>
        /// 角色成员列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PageResult<List<GetUserManageDto>>> GetGroupRoleUserList(GetGroupRoleUserListQueryDto dto)
        {
            if (dto.PageSize <= 0) dto.PageSize = 10;
            if (dto.PageIndex <= 0) dto.PageIndex = 1;

            RefAsync<int> total = 0;
            var list = await _userRoleService.GetGroupUsersList(dto.GroupId, dto.RoleId, dto.PageSize, dto.PageIndex, total);

            //await Logger.AddPlatformLog("获取角色成员列表", LogType.DataAccess);
            
            return new PageResult<List<GetUserManageDto>>(list, total, total % dto.PageSize == 0 ? total / dto.PageSize : total / dto.PageSize + 1, dto.PageSize, dto.PageIndex);

        }

        /// <summary>
        /// 获取角色分组列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<PageResult<List<RoleGroupDto>>> GetGroupRoleList(int pageSize, int pageIndex)
        {
            if (pageSize<=0) pageSize = 10;
            if (pageIndex<=0) pageIndex = 1;

            RefAsync<int> total = 0;
            var list = await _roleService.GetGroupRolesList(pageSize, pageIndex, total);

            //await Logger.AddPlatformLog("分页获取角色分组列表", LogType.DataAccess);
            
            return new PageResult<List<RoleGroupDto>>(list, total, total%pageSize==0 ? total/pageSize : total/pageSize+1, pageSize, pageIndex);

        }

        /// <summary>
        /// 获取角色分组列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<AjaxResult<List<UserRoleGroupDto>>> GetGroupRoleUsersList()
        {
            //await Logger.AddPlatformLog("获取角色分组列表", LogType.DataAccess);
            return new AjaxResult<List<UserRoleGroupDto>>(await _roleService.GetGroupRoleUsersList());
        }


        /// <summary>
        /// 获取权限列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<AjaxResult<List<PermissionDto>>> GetAllPermissions()
        {
            var list = await _rolePermissionService.GetAllPermissionsList();

            //await Logger.AddPlatformLog("获取权限列表", LogType.DataAccess);

            return new AjaxResult<List<PermissionDto>>(list);
        }

        /// <summary>
        /// 获取角色权限
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<AjaxResult<List<PermissionDto>>> GetRolePermissions(int roleId)
        {
            var list = await _rolePermissionService.GetRolePermissionsList(roleId);

            //await Logger.AddPlatformLog("获取角色权限", LogType.DataAccess);
            
            return new AjaxResult<List<PermissionDto>>(list);

        }

        /// <summary>
        /// 获取角色权限菜单
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<AjaxResult<List<PermissionGroupByDto>>> GetRolePermissionsMenu(int roleId)
        {
            //string sql = $"select p.*,pp.Url ModuleUrl,pp.Permission ModuleName from (select p.Id,r.Id `RoleId`,r.`Name` `RoleName`,p.Permission,p.ParentId,p.Url,p.Icon from cloud_role_permission rp left join cloud_permission p on rp.PermissionId = p.Id left join cloud_role r on r.Id = rp.RoleId where RoleId={roleId}  GROUP BY ParentId,Id) p left join cloud_permission pp on p.ParentId=pp.Id";
            //var permissionList = await _dataTableService.GetEntityList<GetRolePermissionsResultDto>(sql);
            //var result = permissionList.GroupBy(e => e.ModuleUrl).ToDictionary(key => key.Key);
            //return new AjaxResult<Dictionary<string, IGrouping<string, GetRolePermissionsResultDto>>>(result);

            string parentSql = $"select distinct p.ParentId,pp.Url ModuleUrl,pp.Permission ModuleName from (select p.Id,r.Id `RoleId`,r.`Name` `RoleName`,p.Permission,p.ParentId,p.Url,p.Icon from cloud_role_permission rp join cloud_permission p on rp.PermissionId = p.Id join cloud_role r on r.Id = rp.RoleId where RoleId={roleId} GROUP BY ParentId,Id) p join cloud_permission pp on p.ParentId=pp.Id ";
            var parentPermissionList = await _dataTableService.GetEntityList<PermissionGroupByDto>(parentSql);
            foreach (var item in parentPermissionList)
            {
                string sql = $"select p.*,p.ParentId,pp.Url ModuleUrl,pp.Permission ModuleName from (select p.Id,r.Id `RoleId`,r.`Name` `RoleName`,p.Permission,p.ParentId,p.Url,p.Icon from cloud_role_permission rp left join cloud_permission p on rp.PermissionId = p.Id left join cloud_role r on r.Id = rp.RoleId where RoleId={roleId} and p.ParentId={item.ParentId} GROUP BY ParentId,Id) p left join cloud_permission pp on p.ParentId=pp.Id ";
                item.PermissionlList=await _dataTableService.GetEntityList<PermissionDataDto>(sql);
            }

            //await Logger.AddPlatformLog("获取角色权限菜单", LogType.DataAccess);

            return new AjaxResult<List<PermissionGroupByDto>>(parentPermissionList);
        }


        /// <summary>
        /// 编辑角色权限
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<bool>> GetRolePermissions(GetRolePermissionsDto dto)
        {
            List<cloud_role_permission> list;
            int effectedRows;
            var RolePermissions = await _rolePermissionService.GetEntitys(e => e.RoleId==dto.RoleId);
            if (RolePermissions.Count<=0)
            {
                //没有该角色的权限
                list = dto.PermissionIdList.Select(permissionId => new cloud_role_permission { RoleId=dto.RoleId, PermissionId=permissionId }).ToList();
                effectedRows = await _rolePermissionService.Adds(list);
                return new AjaxResult<bool>(effectedRows>0);
            }

            var isDelete = await _rolePermissionService.Delete(e => e.RoleId==dto.RoleId);
            if (!isDelete) return new AjaxResult<bool>(false, "删除角色权限失败");

            list = dto.PermissionIdList.Select(permissionId => new cloud_role_permission { RoleId=dto.RoleId, PermissionId=permissionId }).ToList();
            effectedRows = await _rolePermissionService.Adds(list);

            //await Logger.AddPlatformLog("编辑角色权限", LogType.DataOperation);

            return new AjaxResult<bool>(effectedRows>0);
        }



        /// <summary>
        /// 新增角色
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<bool>> AddRole(AddRoleDto dto)
        {
            var role = await _roleService.GetEntity(e => e.GroupId==dto
            .GroupId && e.Name==dto.RoleName);
            if (role != null) return new AjaxResult<bool>(false, "已存在该角色");

            var result = await _roleService.Add(new cloud_role
            {
                GroupId = dto.GroupId,
                Name= dto.RoleName,
            });

            //await Logger.AddPlatformLog("新增角色", LogType.DataOperation);

            return new AjaxResult<bool>(result);
        }

        /// <summary>
        /// 新增分组
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<bool>> AddGroup(AddGroupDto dto)
        {
            var result = await _groupService.Add(new cloud_role_group
            {
                GroupName = dto.GroupName
            });

            //await Logger.AddPlatformLog("新增分组", LogType.DataOperation);

            return new AjaxResult<bool>(result);
        }

    }
}
