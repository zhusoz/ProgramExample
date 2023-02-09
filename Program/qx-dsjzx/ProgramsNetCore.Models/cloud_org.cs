using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Models
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("cloud_org")]
    public partial class cloud_org
    {
           public cloud_org(){


           }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int Id {get;set;}

           /// <summary>
           /// Desc:机构名称
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string Title {get;set;}

           /// <summary>
           /// Desc:上级机构
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? Parent_id {get;set;}

           /// <summary>
           /// Desc:机构完整路径
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Path {get;set;}

           /// <summary>
           /// Desc:机构创建者
           /// Default:
           /// Nullable:False
           /// </summary>           
           public long Creater {get;set;}

           /// <summary>
           /// Desc:创建时间
           /// Default:CURRENT_TIMESTAMP
           /// Nullable:False
           /// </summary>           
           public DateTime Gmt_create {get;set;}

    }
}
