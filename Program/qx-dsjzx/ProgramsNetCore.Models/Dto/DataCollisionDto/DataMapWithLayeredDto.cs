using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Models.Dto.DataCollisionDto
{
    /// <summary>
    /// DataMap携带分层名称
    /// </summary>
    public class DataMapWithLayeredDto
    {
        /// <summary>
        /// 类型名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 表信息
        /// </summary>
        public List<DataMapWithFieldsDto> List { get; set; }
        /// <summary>
        /// 唯一标识
        /// </summary>
        public string GUID { get; set; }
        /// <summary>
        /// 层级
        /// </summary>
        public int Level { get; set; }
    }
}
