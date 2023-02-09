using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Models.Dto.DataCollectDto
{
    public class DataMapManageEditDto
    {
        /// <summary>
        /// Desc:主键
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int Id { get; set; }

        /// <summary>
        /// Desc:模型名称
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Name { get; set; }


        /// <summary>
        /// Desc:数源单位
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int Attribution { get; set; }

        /// <summary>
        /// Desc:分层类别名称
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int Layered { get; set; }

        /// <summary>
        /// Desc:描述
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Describe { get; set; }

        /// <summary>
        /// Desc:关联表
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string AssociativeTable { get; set; }

        /// <summary>
        /// Desc:数据安全类型
        /// Default:
        /// Nullable:True
        /// </summary>     
        public int ModifierType { get; set; }

        
        /// <summary>
        /// Desc:频率类别
        /// Default:
        /// Nullable:True
        /// </summary>     
        public int? Frequency { get; set; }
    }
}
