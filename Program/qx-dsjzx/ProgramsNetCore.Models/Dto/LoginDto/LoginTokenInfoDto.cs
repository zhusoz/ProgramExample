using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Models.Dto.LoginDto
{
    /// <summary>
    /// 信息返回类
    /// </summary>
    public class LoginTokenInfoDto:ResultLoginInfoDto
    {
        public string Token { get; set; }
    }
}
