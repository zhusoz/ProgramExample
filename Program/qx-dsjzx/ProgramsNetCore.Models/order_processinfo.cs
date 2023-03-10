using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Models
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("order_processinfo")]
    public partial class order_processinfo
    {
           public order_processinfo(){


           }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int Id {get;set;}

           /// <summary>
           /// Desc:流程Id
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? ProcessId {get;set;}

           /// <summary>
           /// Desc:意见
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Opinion {get;set;}

           /// <summary>
           /// Desc:节点：0：申请；1：已处理完成；2：经核实无需处理，驳回；3：协商处理；4：申请延期处理；5：已解决；6：未解决；
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? Node {get;set;}

    }
}
