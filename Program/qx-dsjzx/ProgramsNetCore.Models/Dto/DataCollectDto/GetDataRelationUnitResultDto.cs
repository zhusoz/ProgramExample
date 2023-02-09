using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Models.Dto.DataCollectDto
{
    /// <summary>
    /// 涉及数据单位总数dto
    /// </summary>
    public class GetDataRelationUnitResultDto
    {
        /// <summary>
        /// 涉及数据单位总数
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 各单位详情
        /// </summary>
        public List<DataUnitDto> Items { get; set; }

    }

    /// <summary>
    /// 
    /// </summary>
    public class DataUnitDto
    {
        /// <summary>
        /// 数源单位Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 数源单位名称
        /// </summary>
        public string Attribution { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Count { get; set; }


    }
}
