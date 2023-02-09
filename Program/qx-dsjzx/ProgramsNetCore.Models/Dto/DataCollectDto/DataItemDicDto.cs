using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProgramsNetCore.Models.Dto.DataCollectDto
{
    /// <summary>
    /// 数据项字典Dto
    /// </summary>
    public class DataItemDicDto
    {

        /// <summary>
        /// 字段中文名
        /// </summary>
        [Required]
        [Display(Name = "字段中文名")]
        public string CnFieldName { get; set; }

        /// <summary>
        /// 字段英文名称
        /// </summary>
        [Required]
        [Display(Name = "字段英文名称")]
        public string EnFieldName { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        [Required]
        [Display(Name = "数据类型")]
        public string DataType { get; set; }

        /// <summary>
        /// 数据长度
        /// </summary>
        [Required]
        [Display(Name = "数据长度")]
        public string DataLength { get; set; }

        /// <summary>
        /// 字段描述
        /// </summary>
        [Required]
        [Display(Name = "字段描述")]
        public string FieldDescription { get; set; }

        /// <summary>
        /// 是否主键
        /// </summary>
        public bool IsPrimaryKey { get; set; }

        /// <summary>
        /// 共享类型
        /// </summary>
        public bool ShareType { get; set; }


    }
}
