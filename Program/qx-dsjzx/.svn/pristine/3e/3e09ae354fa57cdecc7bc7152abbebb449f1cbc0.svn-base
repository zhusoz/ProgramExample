using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Models
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("cleardata")]
    public partial class cleardata
    {
           public cleardata(){


           }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int Id {get;set;}

           /// <summary>
           /// Desc:清洗规则
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? RuleId {get;set;}

           /// <summary>
           /// Desc:清洗数量
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? CleaningNum {get;set;}

           /// <summary>
           /// Desc:问题数量
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? ProblemNum {get;set;}

           /// <summary>
           /// Desc:清洗时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? ClearTime {get;set;}

    }
}
