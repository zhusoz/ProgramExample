using System;
using System.Linq;
using System.Text;

namespace Models
{
    ///<summary>
    ///
    ///</summary>
    public partial class datatrend
    {
           public datatrend(){


           }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int Id {get;set;}

           /// <summary>
           /// Desc:操作用户Id
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int UserId {get;set;}

           /// <summary>
           /// Desc:源表Id
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int DataMapId {get;set;}

           /// <summary>
           /// Desc:导入/导出的数据量
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int AffectedRows {get;set;}

           /// <summary>
           /// Desc:创建时间
           /// Default:
           /// Nullable:False
           /// </summary>           
           public DateTime CreateTime {get;set;}

           /// <summary>
           /// Desc:0:导入 1:导出
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int Type {get;set;}

    }
}
