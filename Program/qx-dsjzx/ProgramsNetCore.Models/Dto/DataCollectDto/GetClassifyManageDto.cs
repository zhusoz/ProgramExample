using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Models.Dto.DataCollectDto
{
    /// <summary>
    /// 分类分级dto
    /// </summary>
    public class GetClassifyManageDto
    {
        /// <summary>
        /// 流程号Id
        /// </summary>
        public int ApprovalId { get; set; }

        /// <summary>
        /// DataMapId
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 表名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 表英文名
        /// </summary>
        public string EnName { get; set; }

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
