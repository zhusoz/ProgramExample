using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Models.Dto.DataManageDto
{
    /// <summary>
    /// 映射表请求类
    /// </summary>
    public class DataMappingInfoDto
    {
        /// <summary>
        /// 数据表ID
        /// </summary>
        public int DataId { get; set; }
        /// <summary>
        /// 映射表名称
        /// </summary>
        public string TableName { get; set; }
        /// <summary>
        /// 执行人
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Describe { get; set; }
        /// <summary>
        /// 字段
        /// </summary>

        public List<DataMappingFieldDto> Field { get; set; }
    }
}
