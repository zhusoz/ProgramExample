using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Models.Dto.DataCollectDto
{
    /// <summary>
    /// 分级分类详情源表数据项dto
    /// </summary>
    public class GetClassifyManageDataItemsDto
    {

        /// <summary>
        /// 源表Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 源表名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 源表英文名
        /// </summary>
        public string EnName { get; set; }

        /// <summary>
        /// 关联表名称
        /// </summary>
        public string AssociativeTable { get; set; }

        /// <summary>
        /// 信息摘要
        /// </summary>
        public string InfoSummary { get; set; }

        /// <summary>
        /// 数据来源
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public string LinkPerson { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string LinkPhone { get; set; }

        /// <summary>
        /// 所属应用系统名称
        /// </summary>
        public string ApplicationSystemName { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 数源单位
        /// </summary>
        public string Attribution { get; set; }

        /// <summary>
        /// 源表所有数据项
        /// </summary>
        public List<ResultDataItemDicDto> DataItems { get; set; }
    }
}
