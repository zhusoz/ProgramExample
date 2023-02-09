using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Models
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("conn_orirule_rule")]
    public partial class conn_orirule_rule
    {
           public conn_orirule_rule(){


           }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int Id {get;set;}

           /// <summary>
           /// Desc:规则Id
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? RuleId {get;set;}

           /// <summary>
           /// Desc:原始规则Id
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? OId {get;set;}

    }
}
