using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Models
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("tabletotable")]
    [Serializable]
    public partial class tabletotable
    {
           public tabletotable(){


           }
           /// <summary>
           /// Desc:表与表之间的数据外键
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int Id {get;set;}

           /// <summary>
           /// Desc:A表
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string TableNameA {get;set;}

           /// <summary>
           /// Desc:A表的关联字段
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string TableFieldA {get;set;}

           /// <summary>
           /// Desc:B表
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string TableNameB {get;set;}

           /// <summary>
           /// Desc:B表的关联字段
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string TableFieldB {get;set;}

           /// <summary>
           /// Desc:模型或者任务ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? ModelId {get;set;}

           /// <summary>
           /// Desc:1：模型；2：任务
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? Type {get;set;}

           /// <summary>
           /// Desc:A表模式名
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string PatternNameA {get;set;}

           /// <summary>
           /// Desc:B表模式名
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string PatternNameB {get;set;}

    }
}
