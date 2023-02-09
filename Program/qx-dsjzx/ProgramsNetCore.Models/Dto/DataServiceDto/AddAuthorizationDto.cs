using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Models.Dto.DataServiceDto
{
    /// <summary>
    /// 添加权限
    /// </summary>
    public class AddAuthorizationDto
    {
        /// <summary>
        /// serviceapplicationtaskinfoId
        /// </summary>
        public int[] Ids { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        public int Role { get; set; }
        /// <summary>
        /// 1 通过 2 驳回
        /// </summary>
        public int? Status { get; set; }
    }
}
