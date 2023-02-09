using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Models
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("conditions")]
    public partial class conditions
    {
           public conditions(){


           }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int Id {get;set;}

           /// <summary>
           /// Desc:条件
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ConditionsName {get;set;}

           /// <summary>
           /// Desc:条件值
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ConditionsValue {get;set;}

           /// <summary>
           /// Desc:条件类型
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? Type {get;set;}

           /// <summary>
           /// Desc:输入框数量
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? InputCount {get;set;}

           /// <summary>
           /// Desc:解释
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Describe {get;set;}

           /// <summary>
           /// Desc:替换符
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Replace {get;set;}

    }
}
