using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Models.Dto.UserManageDto
{
    /// <summary>
    /// 角色权限模块Dto
    /// </summary>
    public class GetRolePermissionsResultDto
    {
        /// <summary>
        /// 权限Id
        /// </summary>
        public int Id { get; set; }

        //public int RoleId { get; set; }

        //public string RoleName { get; set; }

        /// <summary>
        /// 权限名称
        /// </summary>
        public string Permission { get; set; }

        /// <summary>
        /// 父级模块id
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// 模块url
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 父级模块url
        /// </summary>
        public string ModuleUrl { get; set; }


        /// <summary>
        /// 所属模块名称
        /// </summary>
        public string ModuleName { get; set; }

        /// <summary>
        /// Icon图标Url
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 权限列表
        /// </summary>
        public List<GetRolePermissionsResultDto> PermissionList { get; set; }
    }
}
