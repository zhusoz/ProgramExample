using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Models.Dto.DataCollectDto
{
    /// <summary>
    /// 分类分级管理QueryDto
    /// </summary>
    public class ClassifyManageEditDto
    {
        /// <summary>
        /// 源表Id
        /// </summary>
        public int DataMapId { get; set; }

        /// <summary>
        /// 信息资源名称
        /// </summary>
        //public string Name { get; set; }

        ///// <summary>
        ///// 区域
        ///// </summary>
        //public string Region { get; set; }


        /// <summary>
        /// 分层类型
        /// </summary>
        public int LayeredType { get; set; }

        /// <summary>
        /// 修饰类型
        /// </summary>
        public int ModifierType { get; set; }

    }
}
