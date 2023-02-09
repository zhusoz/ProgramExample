using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Models.Dto.LoginDto
{
    /// <summary>
    /// 登录请求类
    /// </summary>
    public class LoginDto
    {
        /// <summary>
        /// 账户
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
    }
}
