﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Models.Dto.DataServiceDto
{
    /// <summary>
    /// 申请列表视图模型
    /// </summary>
    public class ResultApplicationListDto
    {
        /// <summary>
        /// 任务Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// datamapId
        /// </summary>
        public int TableId { get; set; }
        /// <summary>
        /// 数据名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 区域
        /// </summary>
        public string Area { get; set; }
        /// <summary>
        /// 数源单位
        /// </summary>
        public string SourceUnit { get; set; }
        /// <summary>
        /// 申请人
        /// </summary>
        public string Applicant { get; set; }
        /// <summary>
        /// 申请日期
        /// </summary>
        public DateTime? ApplyDate { get; set; }
        /// <summary>
        /// 状态 null待申请 0待授权 1已授权 2已驳回
        /// </summary>
        public int? Status { get; set; }
        /// <summary>
        /// 申请时间
        /// </summary>
        public int? ApplyTime { get; set; }
    }
}
