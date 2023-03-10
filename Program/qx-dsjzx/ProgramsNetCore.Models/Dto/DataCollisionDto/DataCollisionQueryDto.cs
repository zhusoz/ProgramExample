using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Models.Dto.DataCollisionDto
{
    /// <summary>
    /// 数据碰撞请求类
    /// </summary>
    public class DataCollisionQueryDto : PageDto
    {
        /// <summary>
        /// 建模单位
        /// </summary>
        public string Tissue { get; set; }
        /// <summary>
        /// 涉及领域
        /// </summary>
        public string Region { get; set; }
        /// <summary>
        /// 模型名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 推广状态：0：不推广；1：推广
        /// </summary>
        public int ExtensionStatus { get; set; } = -1;
        /// <summary>
        /// 状态,多个的话用逗号隔开；1：未审核；2：已审核3：驳回
        /// </summary>
        public string Status { get;set; }
        /// <summary>
        /// 删除状态:0：未删除；1：已删除
        /// </summary>
        public int Deleted { get; set; } = -1;
        /// <summary>
        /// 模式名.
        /// </summary>
        public string PatternName { get; set; } = "data_sharing_main";
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; }
        /// <summary>
        /// 类型；1：模型；2：任务
        /// </summary>
        public int Type { get; set; } = -1;
        /// <summary>
        /// 模型或者任务ID
        /// </summary>
        public int ModelId { get; set; } = -1;
        /// <summary>
        /// A表表名
        /// </summary>
        public string TableNameA { get; set; }
        /// <summary>
        /// B表表名
        /// </summary>
        public string TableNameB { get; set; }
        /// <summary>
        /// 主题库还是私有库。1：主题库；2：私有库
        /// </summary>
        public int PublicOrPrivate { get; set; } = -1;

        /// <summary>
        /// 拓扑图元素节点集合
        /// </summary>
        public List<TopologyDto> TopologyDtos { get; set; }

        /// <summary>
        /// 模型审批用户
        /// </summary>
        public string Approver { get; set; }
        /// <summary>
        /// 页面上要执行的sql.
        /// </summary>
        public string Sql { get; set; }
        /// <summary>
        /// 预览的时候查询的条件
        /// </summary>
        public List<DataConditionDto> DataConditions { get; set; }
    }
}
