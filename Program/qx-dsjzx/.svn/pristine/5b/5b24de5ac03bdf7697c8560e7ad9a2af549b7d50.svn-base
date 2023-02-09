using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Models
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("cloud_permission")]
    public partial class cloud_permission
    {
        public cloud_permission()
        {


        }
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        /// <summary>
        /// Desc:权限名称
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Permission { get; set; }

        /// <summary>
        /// Desc:父级模块Id
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? ParentId { get; set; }

        /// <summary>
        /// Desc:链接Url
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Url { get; set; }

        /// <summary>
        /// Desc:父级模块
        /// Default:
        /// Nullable:True
        /// </summary>    
        [Navigate(NavigateType.OneToOne, nameof(ParentId))]//设置导航 一对一
        public cloud_permission Parent { get; set; }

        /// <summary>
        /// Desc:树型递归查询
        /// Default:
        /// Nullable:True
        /// </summary>
        [SqlSugar.SugarColumn(IsIgnore = true)]
        public List<cloud_permission> Child { get; set; }

    }
}
