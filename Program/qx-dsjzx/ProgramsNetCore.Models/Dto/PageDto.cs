﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Models.Dto
{
    /// <summary>
    /// 分页请求基类
    /// </summary>
    public class PageDto
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 页数
        /// </summary>
        public int PageSize { get; set; }
    }
}
