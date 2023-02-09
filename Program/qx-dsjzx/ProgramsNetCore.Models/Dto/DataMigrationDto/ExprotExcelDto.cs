using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Models.Dto.DataMigration
{
    /// <summary>
    /// 迁移导出Excel请求类
    /// </summary>
    public class ExprotExcelDto
    {
        public List<int> TaskId { get; set; }
    }
}
