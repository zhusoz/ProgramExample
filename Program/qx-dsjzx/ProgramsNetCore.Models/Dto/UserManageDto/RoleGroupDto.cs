using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Models.Dto.UserManageDto
{
    /// <summary>
    /// 角色分组dto
    /// </summary>
    public class RoleGroupDto
    {
        /// <summary>
        /// 分组Id
        /// </summary>
        public int GroupId { get; set; }

        /// <summary>
        /// 分组名称
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// 角色数组
        /// </summary>
        public List<RoleDto> RoleList { get; set; }
    }
}
