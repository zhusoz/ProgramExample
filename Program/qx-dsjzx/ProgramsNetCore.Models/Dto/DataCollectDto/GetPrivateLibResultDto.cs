using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Models.Dto.DataCollectDto
{
    /// <summary>
    /// 私有库查询dto
    /// </summary>
    public class GetPrivateLibResultDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 数据源表名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 数源单位
        /// </summary>
        public string Attribution { get; set; }

        /// <summary>
        /// 数据来源
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// 分层类型
        /// </summary>
        public string LayeredName { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public int? ExpireTime { get; set; }

        /// <summary>
        /// Guid
        /// </summary>
        public string Guid { get; set; }

        /// <summary>
        /// 模式名
        /// </summary>
        public string PatternName { get; set; }

    }
}
