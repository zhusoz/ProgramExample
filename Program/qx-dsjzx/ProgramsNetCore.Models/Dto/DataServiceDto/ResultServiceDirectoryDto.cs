using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Models.Dto.DataServiceDto
{

    /// <summary>
    /// 服务目录返回类
    /// </summary>
    public class ResultServiceDirectoryDto
    {

        /// <summary>
        /// Desc:主键
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int Id { get; set; }

        /// <summary>
        /// Desc:英文名称
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string EnName { get; set; }

        /// <summary>
        /// Desc:源表名称
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Name { get; set; }

        /// <summary>
        /// Desc:编号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Guid { get; set; }

        /// <summary>
        /// Desc:数源单位
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Attribution { get; set; }

        /// <summary>
        /// Desc:分层类别名称
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string LayeredTypeName { get; set; }

        /// <summary>
        /// Desc:数据安全类型
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string DataSafeType { get; set; }

        /// <summary>
        /// Desc:描述
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Describe { get; set; }


        /// <summary>
        /// Desc:关联表名称
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string AssociativeTable { get; set; }

        /// <summary>
        /// Desc:频率类别
        /// Default:
        /// Nullable:True
        /// </summary>     
        public string FrequencyName { get; set; }

        /// <summary>
        /// Desc:最后更新时间
        /// Default:
        /// Nullable:True
        /// </summary>     
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 访问量
        /// </summary>
        public int Traffic { get; set; }

        /// <summary>
        /// 申请量
        /// </summary>
        public int ApplicationNum { get; set; }
        /// <summary>
        /// 是否映射
        /// </summary>
        public string MappingStr { get; set; }
        /// <summary>
        /// 是否拆分
        /// </summary>

        public string SplitStr { get; set; }
        /// <summary>
        /// 映射表名
        /// </summary>
        public string MappingName { get; set; }

        /// <summary>
        /// 是否转数据服务
        /// </summary>
        public bool?  IsDataService { get; set; }

    }
}
