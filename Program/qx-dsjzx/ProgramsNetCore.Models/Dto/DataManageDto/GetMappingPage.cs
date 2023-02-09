using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Models.Dto.DataManageDto
{
    /// <summary>
    /// 获取映射列表请求类
    /// </summary>
    public class GetMappingPage:PageDto
    {
        /// <summary>
        /// 部门Id
        /// </summary>
        public int? DepartmentId { get; set; }
    }
}
