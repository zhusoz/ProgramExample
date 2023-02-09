using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Models.Dto.DataMigration
{
    /// <summary>
    /// 获取数据信息请求类
    /// </summary>
    public class GetDataInfoPageDto:PageDto
    {
        /// <summary>
        /// 数据Id
        /// </summary>
        public int DataId { get; set; }
        /// <summary>
        /// 登录人Id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 条件：默认为null可不传
        /// </summary>
        public string Conditions { get; set; } = string.Empty;
        /// <summary>
        /// 是否分表：默认为false
        /// </summary>
        public bool IsChild { get; set; } = false;
        /// <summary>
        /// 是否治理：默认为false
        /// </summary>
        public bool IsManage { get; set; } = false;
    }
}
