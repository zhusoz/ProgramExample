using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Models.Dto.DataCollectDto
{
    public class ResultCollectDataStatisticsDto
    {
        /// <summary>
        /// 源表数据总量
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 上架源表数量
        /// </summary>
        public int OnCount { get; set; }


        /// <summary>
        /// 下架源表数量
        /// </summary>
        public int OffCount { get; set; }

        /// <summary>
        /// 未上架源表数量
        /// </summary>
        public int TodoCount { get; set; }

    }
}
