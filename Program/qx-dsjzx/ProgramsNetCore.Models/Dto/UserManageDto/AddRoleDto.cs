using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Models.Dto.UserManageDto
{
    /// <summary>
    /// 新增角色Dto
    /// </summary>
    public class AddRoleDto
    {
        /// <summary>
        /// 分组Id
        /// </summary>
        public int GroupId { get; set; }
        
        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }
        
        /// <summary>
        /// 权限列表
        /// </summary>
        //public int[] PermissionIdList { get; set; }

    }
}
