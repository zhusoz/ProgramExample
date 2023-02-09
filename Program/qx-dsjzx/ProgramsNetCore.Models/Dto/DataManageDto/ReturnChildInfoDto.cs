using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Models.Dto.DataManageDto
{
    /// <summary>
    /// 拆分表信息返回类
    /// </summary>
    public class ReturnChildInfoDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>

        public string Name { get; set; }
        /// <summary>
        /// 中文拆分条件
        /// </summary>
        public string Cn_Conditions { get; set; }
        /// <summary>
        /// 字段注解
        /// </summary>
        public List<ReturnChildFieldsDto> Fields { get; set; }
    }
}
