using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Models
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("hierarchy")]
    public partial class hierarchy
    {
           public hierarchy(){


           }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int Id {get;set;}

           /// <summary>
           /// Desc:任务Id
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int TaskId {get;set;}

           /// <summary>
           /// Desc:用户Id
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int UserId {get;set;}

           /// <summary>
           /// Desc:父Id
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? Pid {get;set;}

    }
}
