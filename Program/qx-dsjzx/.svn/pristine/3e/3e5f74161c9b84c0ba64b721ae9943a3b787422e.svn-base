using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Models.Dto.DataSecurity
{
    /// <summary>
    /// 加密请求类
    /// </summary>
    public class DataEncryptionInfoDto
    {
        /// <summary>
        /// 数据表编号
        /// </summary>
        public string GUID { get; set; }
        /// <summary>
        /// 加密对象
        /// </summary>
        public List<string> Obj { get; set; }
        /// <summary>
        /// 加密类型 1：字段加密 2：表加密
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 加密key
        /// </summary>
        public string Key { get; set; }

       /// <summary>
       /// 加密类型：1：key加密；2：自动加密
       /// </summary>
        public int SecoretMode { get; set; }
    }
}
