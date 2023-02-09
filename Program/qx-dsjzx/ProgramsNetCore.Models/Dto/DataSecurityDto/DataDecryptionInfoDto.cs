using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Models.Dto.DataSecurity
{
    /// <summary>
    /// 解密信息请求类
    /// </summary>
    public class DataDecryptionInfoDto
    {
        public string GUID { get; set; }
        /// <summary>
        /// 解密开始时间
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 解密结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 解密类型：1：解密；2：临时解密；
        /// </summary>
        public int Type { get; set; }
    }
}
