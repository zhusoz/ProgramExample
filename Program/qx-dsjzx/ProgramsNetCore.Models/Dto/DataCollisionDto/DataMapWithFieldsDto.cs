﻿using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Models.Dto.DataCollisionDto
{
    /// <summary>
    /// DataMap携带表结构
    /// </summary>
    public class DataMapWithFieldsDto:datamap
    {
        /// <summary>
        /// 类型名称
        /// </summary>
        public string LayeredTypeStr { get; set; }
        /// <summary>
        /// 表结构
        /// </summary>
        public List<TableDto> List { get; set; }
        /// <summary>
        /// 层级
        /// </summary>
        public int Level { get; set; }

    }
}
