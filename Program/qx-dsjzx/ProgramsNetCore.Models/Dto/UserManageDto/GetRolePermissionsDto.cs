using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Models.Dto.UserManageDto
{
    /// <summary>
    /// 编辑角色权限Dto
    /// </summary>
    public class GetRolePermissionsDto
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// 权限id数组
        /// </summary>
        public int[] PermissionIdList { get; set; }
    }
}
