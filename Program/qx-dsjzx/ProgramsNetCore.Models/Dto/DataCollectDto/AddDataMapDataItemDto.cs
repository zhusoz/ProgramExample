using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Models.Dto.DataCollectDto
{
    /// <summary>
    /// 源表新增数据项dto
    /// </summary>
    public class AddDataMapDataItemDto:DataItemDicDto
    {
        /// <summary>
        /// 源表id
        /// </summary>
        public int DataMapId { get; set; }
    }
}
