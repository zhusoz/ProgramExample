using System;
using System.Linq;
using System.Text;

namespace Models
{
    ///<summary>
    ///
    ///</summary>
    public partial class log_error
    {
           public log_error(){


           }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int Id {get;set;}

           /// <summary>
           /// Desc:源表Id
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? DataMap {get;set;}

           /// <summary>
           /// Desc:异常类型:[0:瞬时调用异常 1:调用空值异常 2:调用时间异常 3:调用频次异常 4:请求秘钥获取异常 5:未时调用异常
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? ErrorType {get;set;}

           /// <summary>
           /// Desc:异常生成时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? CreateTime {get;set;}

    }
}
