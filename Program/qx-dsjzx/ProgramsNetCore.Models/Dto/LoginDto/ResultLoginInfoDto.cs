using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Models.Dto.LoginDto
{
    /// <summary>
    /// 登录信息返回类
    /// </summary>
    public class ResultLoginInfoDto
    {
        public int Id { get; set; }

        /// <summary>
        /// 账户
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Real_Name { get; set; }
        /// <summary>
        /// 部门Id
        /// </summary>
        public int DepartmentId { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string Department { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public string Role { get; set; }

    }
}
