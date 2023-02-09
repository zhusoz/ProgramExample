using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Models.Dto.DataSecurity
{
    /// <summary>
    /// 数据列表请求类
    /// </summary>
    public class DataInfoDto:PageDto
    {
        /// <summary>
        /// 1：全部列表；2：加密列表；3：解密列表
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 部门Id
        /// </summary>
        public int DepartmentId { get; set; }
        /// <summary>
        /// 查询
        /// </summary>

        public string LikeStr { get; set; }
    }
}
