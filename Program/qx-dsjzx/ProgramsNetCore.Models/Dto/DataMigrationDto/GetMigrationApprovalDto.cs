using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Models.Dto.DataMigration
{
    /// <summary>
    /// 获取迁移审批请求类
    /// </summary>
    public  class GetMigrationApprovalDto:PageDto
    {
        public List<int> Id { get; set; }
    }
}
