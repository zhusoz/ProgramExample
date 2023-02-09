using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Models.Dto.UserManageDto
{
    /// <summary>
    /// 人员管理请求视图dto
    /// </summary>
    public class GetUserManageListDto:PageDto
    {

        /// <summary>
        /// 角色Id
        /// </summary>
        public int? RoleId { get; set; }

        /// <summary>
        /// 部门Id
        /// </summary>
        public int? DepartId { get; set; }

    }
}
