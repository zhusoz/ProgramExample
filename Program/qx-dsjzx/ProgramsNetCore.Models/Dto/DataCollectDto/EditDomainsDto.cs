using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Models.Dto.DataCollectDto
{
    /// <summary>
    /// 领域编辑dto
    /// </summary>
    public class EditDomainsDto
    {
        /// <summary>
        /// type:[0:数据来源 1:分层类型]
        /// </summary>
        public int TypeId { get; set; }

        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

    }
}
