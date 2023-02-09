using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Models.Dto.Test
{
    /// <summary>
    /// 数据解密类
    /// </summary>
    public class XORDEnDto
    {
        /// <summary>
        /// 数据库地址
        /// </summary>
        public string DBHost { get; set; }
        /// <summary>
        /// 数据库类型：0：Mysql；1：SqlServer；2：Sqlite；5：达梦；6：人大金仓；7：Oracle;
        /// </summary>
        public int DBType { get; set; }
        /// <summary>
        /// 端口
        /// </summary>
        public string Port { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Passwrod { get; set; }
        /// <summary>
        /// 数据库
        /// </summary>
        public string DataBase { get; set; }
        /// <summary>
        /// 操作类型：1：加密；2：解密；
        /// </summary>
        public int ActionType { get; set; }
        /// <summary>
        /// 指定数据库
        /// </summary>
        public string ToDataBase { get; set; }
        /// <summary>
        /// 数据表信息
        /// </summary>

        public List<XORTableInfoDto> TableInfo  { get; set; }

        /// <summary>
        /// 字符集：默认utf8
        /// </summary>
        public string CharacterSet { get; set; } = "utf8";
    }
}
