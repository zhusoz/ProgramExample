using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Models.Dto.DataManageDto
{
    /// <summary>
    /// 关联个性化规则请求类
    /// </summary>
    public class JoinRuleDto
    {

        /// <summary>
        /// 规则id
        /// </summary>
        public List<int> RuleId { get; set; }
        /// <summary>
        /// 个性化规则Id
        /// </summary>
        public int OriRuleId { get; set; }
    }
}
