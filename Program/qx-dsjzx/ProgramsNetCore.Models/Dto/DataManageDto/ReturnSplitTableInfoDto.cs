using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Models.Dto.DataManageDto
{
    /// <summary>
    /// 拆分表信息返回类
    /// </summary>
    public class ReturnSplitTableInfoDto
    {

        /// <summary>
        /// DataMapId
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 中文名
        /// </summary>
        public string CN_Name { get; set; }
        /// <summary>
        /// 英文名
        /// </summary>
        public string EN_Name { get; set; }

        /// <summary>
        /// 关联表
        /// </summary>
        public string AssociativeTable { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 数源单位名称
        /// </summary>
        public string Attribution { get; set; }

        /// <summary>
        /// 分层类型
        /// </summary>
        public string LayerName { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        public string ModifyName { get; set; }
    }
}
