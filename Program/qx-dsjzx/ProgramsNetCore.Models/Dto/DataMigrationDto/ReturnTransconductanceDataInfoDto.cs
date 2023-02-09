using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Models.Dto.DataMigrationDto
{
    /// <summary>
    /// 互导数据详情返回类
    /// </summary>
    public class ReturnTransconductanceDataInfoDto
    {
        public int Id { get; set; }
        /// <summary>
        /// 表名
        /// </summary>
        public string TableNameCn { get; set; }
        /// <summary>
        /// 链接
        /// </summary>
        public string HostLink { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 部门
        /// </summary>
        public string Department { get; set; }
        /// <summary>
        /// 导出类型code
        /// </summary>
        public int ExportType { get; set; }
        /// <summary>
        /// 导出类型名称
        /// </summary>
        public string ExportTypeInfo { get; set; }
        /// <summary>
        /// 执行状态
        /// </summary>
        public string IsImplementInfo { get; set; }
        /// <summary>
        /// 执行状态code
        /// </summary>
        public int IsImplement { get; set; }
        /// <summary>
        /// 数据库类型code
        /// </summary>
        public int DbType { get; set; }
        /// <summary>
        /// 数据库类型
        /// </summary>
        public string DbTypeInfo { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 子集
        /// </summary>
        public List<ReturnTransDataChildInfoDto> ChildList { get; set; }

    }
}
