using Models;
using ProgramsNetCore.Models.Dto.DataManageDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Models.Dto.DataCollisionDto
{
    /// <summary>
    /// 模型目录对外实体
    /// </summary>
    public class DataTaskDto: datatask
    {
        /// <summary>
        /// 私有库返回实体
        /// </summary>
        public List<SheetDto> PrivateSheets { get; set; }
        /// <summary>
        /// 主题库返回实体
        /// </summary>
        public List<SheetDto> PublicSheets { get; set; }
        /// <summary>
        /// 保存时携带的私有库
        /// </summary>
        public List<DataModelPrivateSheetDto> DataModelPrivateSheetDtos { get; set; }
        /// <summary>
        /// 保存时携带的主题库
        /// </summary>
        public List<DataModelPublicSheetDto> DataModelPublicSheetDtos { get; set; }
        /// <summary>
        /// 该模型、任务选择的私有库。
        /// </summary>
        public List<DataMapCollectDto> PrivateTables { get; set; }
        /// <summary>
        /// 该模型、任务选择的主题库。
        /// </summary>
        public List<DataMapCollectDto> PublicTables { get; set; }
    }
}
