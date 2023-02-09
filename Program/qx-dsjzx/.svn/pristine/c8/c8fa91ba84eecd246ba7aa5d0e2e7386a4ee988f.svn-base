using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Models
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("originalrule")]
    public partial class originalrule
    {
           public originalrule(){


           }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int Id {get;set;}

           /// <summary>
           /// Desc:规则名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Name {get;set;}

           /// <summary>
           /// Desc:规则值
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string RuleValue {get;set;}

           /// <summary>
           /// Desc:规则类型：1：原生规则；2：个性化规则
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? RuleType {get;set;}

    }
}
