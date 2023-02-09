using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Models.Dto.DataCollectDto
{
    public class GetSourceTableListPageDto : PageDto
    {
        
        /// <summary>
        /// 数源单位id
        /// </summary>
        public int? DataSourceUnitId { get; set; }
        
        /// <summary>
        /// 分层类别id
        /// </summary>
        public int? LayeredCategoryId { get; set; }
        
        /// <summary>
        /// 更新频率Id
        /// </summary>
        public int? UpdateId { get; set; }

        /// <summary>
        /// 关键字
        /// </summary>
        public string KeyWord { get; set; }
        /// <summary>
        /// 部门Id
        /// </summary>

        public int? DepartmentId { get; set; }
    }
}
