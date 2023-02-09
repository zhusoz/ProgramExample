using ProgramsNetCore.Models.Dto.DataManageDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Models.Dto.HomeDto
{
    /// <summary>
    /// 数据检索结果dto
    /// </summary>
    public class SearchResultDto
    {

        /// <summary>
        /// 查询结果
        /// </summary>
        public List<DataMapCollectDto> Items { get; set; }

        /// <summary>
        /// 左侧相关结果信息
        /// </summary>
        //public List<SearchResultGroupByDto> RelationItems { get; set; }


    }

    /// <summary>
    /// 查询结果分组
    /// </summary>
    public class SearchResultGroupByDto
    {
        /// <summary>
        /// id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 数源单位名称
        /// </summary>
        public string Attribution { get; set; }

        /// <summary>
        /// 数源单位下的分组详情
        /// </summary>
        public List<SearchResultGroupByLayeredDto> Items { get; set; }

    }

    /// <summary>
    /// 数源单位下不同领域相关结果
    /// </summary>
    public class SearchResultGroupByLayeredDto
    {

        /// <summary>
        /// 领域名称
        /// </summary>
        public string Layered { get; set; }

        /// <summary>
        /// 相关结果数量
        /// </summary>
        public int Count { get; set; }

    }
}
