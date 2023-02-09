using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Models
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("secret_child")]
    public partial class secret_child
    {
           public secret_child(){


           }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int id {get;set;}

           /// <summary>
           /// Desc:父ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? Parent_id {get;set;}

           /// <summary>
           /// Desc:加密字段
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SecretKey {get;set;}

    }
}
