using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Models.Dto.DataMigration
{
    /// <summary>
    /// 修改权限信息请求类
    /// </summary>
    public class UpdateAuthorityDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public int DataId { get; set; }
        /// <summary>
        /// 用户
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
    }
}
