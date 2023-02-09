using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Models
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("files")]
    public partial class files
    {
           public files(){


           }
           /// <summary>
           /// Desc:附件表
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int Id {get;set;}

           /// <summary>
           /// Desc:附件类型。1：审计移送
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? Type {get;set;}

           /// <summary>
           /// Desc:路径
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Path {get;set;}

           /// <summary>
           /// Desc:主记录id
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? Parent_id {get;set;}

           /// <summary>
           /// Desc:创建时间
           /// Default:CURRENT_TIMESTAMP
           /// Nullable:True
           /// </summary>           
           public DateTime? Createtime {get;set;}

           /// <summary>
           /// Desc:文件名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Name {get;set;}

    }
}
