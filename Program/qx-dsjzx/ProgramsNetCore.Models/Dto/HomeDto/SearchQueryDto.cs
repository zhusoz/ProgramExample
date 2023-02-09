using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Models.Dto.HomeDto
{
    /// <summary>
    /// 首页检索dto
    /// </summary>
    public class SearchQueryDto:PageDto
    {
        /// <summary>
        /// 关键字
        /// </summary>
        public string KeyWord { get; set; }

        /// <summary>
        /// 排序字段[0:相关性 1:最近七天  按照相关性排序无升序/降序]
        /// </summary>
        public int OrderByField { get; set; }

        /// <summary>
        /// 排序类型[desc:降序 asc:升序  默认参数为asc]
        /// </summary>
        public string OrderByType { get; set; }



        ///// <summary>
        ///// 数源单位
        ///// </summary>
        //public int Attribution { get; set; }

        ///// <summary>
        ///// 分层类型
        ///// </summary>
        //public int Layered { get; set; }

    }

    /// <summary>
    /// 左侧模块dto
    /// </summary>
    public class SearchRelationQueryDto : PageDto
    {
        /// <summary>
        /// 关键字
        /// </summary>
        public string KeyWord { get; set; }
    }
}
