using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Models.Dto.DataCollectDto
{
    /// <summary>
    /// 领域管理查询dto
    /// </summary>
    public class GetDomainsQueryDto : PageDto
    {

        /// <summary>
        /// type:[0:数据来源 1:分层类型]
        /// </summary>
        public int TypeId { get; set; }

        /// <summary>
        /// 关键字
        /// </summary>
        public string KeyWord { get; set; }

    }
}
