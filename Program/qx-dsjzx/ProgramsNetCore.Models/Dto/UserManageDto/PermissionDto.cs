using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Models.Dto.UserManageDto
{
    /// <summary>
    /// 权限dto
    /// </summary>
    public class PermissionDto
    {
        /// <summary>
        /// 权限Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 权限名称
        /// </summary>
        public string Permission { get; set; }

        /// <summary>
        /// url
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 子权限
        /// </summary>
        public List<PermissionDto> PermissionList { get; set; }
    }
}
