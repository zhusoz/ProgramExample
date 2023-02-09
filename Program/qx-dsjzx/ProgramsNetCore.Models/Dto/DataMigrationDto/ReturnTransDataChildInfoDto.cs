using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Models.Dto.DataMigrationDto
{
    /// <summary>
    /// 互导数据子类返回类
    /// </summary>
    public class ReturnTransDataChildInfoDto
    {
        public int Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 数源单位
        /// </summary>
        public string  Attribution { get; set; }
    }
}
