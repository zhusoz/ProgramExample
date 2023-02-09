using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Models.Dto.DataCollisionDto
{
    /// <summary>
    /// 用户信息返回类
    /// </summary>
    public class ReturnUserInfoDto
    {
        /// <summary>
        /// id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string  UserName { get; set; }
    }
}
