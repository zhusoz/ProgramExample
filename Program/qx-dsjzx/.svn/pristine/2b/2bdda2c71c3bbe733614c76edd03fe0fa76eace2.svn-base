using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProgramsNetCore.Models.Dto.DataCollectDto
{
    /// <summary>
    /// 新增申请表dto
    /// </summary>
    public class AddApplyTableDto
    {
        /// <summary>
        /// 源表名称
        /// </summary>
        //[Required]
        public string DataMapName { get; set; }

        /// <summary>
        /// 源表英文名称
        /// </summary>
        //[Required]
        public string DataMapEnName { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 是否私有
        /// </summary>
        public bool IsPrivate { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime? ExpireTime { get; set; }

        /// <summary>
        /// 数据单位Id
        /// </summary>
        //[Required]
        public int AttributionId { get; set; }

        /// <summary>
        /// 领域分类Id
        /// </summary>
        //[Required]
        public int LayeredId { get; set; }

        /// <summary>
        /// 更新频率Id
        /// </summary>
        //[Required]
        public int FrequencyId { get; set; }

        /// <summary>
        /// 数据来源
        /// </summary>
        //[Required]
        public string Source { get; set; }

        /// <summary>
        /// 所属应用系统名称
        /// </summary>
        //[Required]
        public string ApplicationSystemName { get; set; }

        /// <summary>
        /// 数据类型Id
        /// </summary>
        //[Required]
        public int DataTypeId { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        [Required]
        public string LinkPerson { get; set; }

        /// <summary>
        /// 联系人电话
        /// </summary>
        //[Phone]
        public string LinkPhone { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        //[Required]
        public string Description { get; set; }

        /// <summary>
        /// 信息摘要
        /// </summary>
        //[Required]
        public string InfoSummary { get; set; }

        /// <summary>
        /// 数据项
        /// </summary>
        [Required]
        public List<DataItemDicDto> DataItems { get; set; }

    }


    /// <summary>
    /// 过期时间枚举dto
    /// </summary>
    public enum ExpireTimeEnum
    {
        /// <summary>
        /// None
        /// </summary>
        None,

        /// <summary>
        /// 一天
        /// </summary>
        OneDay,

        /// <summary>
        /// 两天
        /// </summary>
        TwoDay,

        /// <summary>
        /// 三天
        /// </summary>
        ThreeDay,

        /// <summary>
        /// 四天
        /// </summary>
        FourDay,

        /// <summary>
        /// 五天
        /// </summary>
        FiveDay,

        /// <summary>
        /// 六天
        /// </summary>
        SixDay,

        /// <summary>
        /// 七天
        /// </summary>
        SevenDay
    }

}
