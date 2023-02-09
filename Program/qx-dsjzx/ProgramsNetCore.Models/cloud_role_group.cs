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
    [SugarTable("cloud_role_group")]
    public partial class cloud_role_group
    {
        public cloud_role_group()
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
        /// Desc:分组名称
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string GroupName { get; set; }

        /// <summary>
        /// 分组角色列表
        /// </summary>
        [Navigate(NavigateType.OneToMany, nameof(cloud_role.GroupId))]
        public List<cloud_role> Roles { get; set; }//注意禁止给books手动赋值

    }
}
