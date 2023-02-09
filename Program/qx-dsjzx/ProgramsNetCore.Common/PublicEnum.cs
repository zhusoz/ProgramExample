using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Common
{
    public static class PublicEnum
    {
        /// <summary>
        /// 授权类型
        /// </summary>
        public  enum AuthorizeType
        {
            /// <summary>
            /// 系统操作员
            /// </summary>
             S_Operator=4,
             /// <summary>
             /// 普通管理员
             /// </summary>
            G_Admin=6,
            /// <summary>
            /// 安全保密员
            /// </summary>
S_Security=2,
/// <summary>
/// 系统审计员
/// </summary>
S_Auditor=3,
/// <summary>
/// 系统管理员
/// </summary>
S_Admin=1,
/// <summary>
/// 超级管理员
/// </summary>
Admin=5,

        }
    }
}
