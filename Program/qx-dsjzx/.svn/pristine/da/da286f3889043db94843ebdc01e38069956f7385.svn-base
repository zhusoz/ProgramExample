using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Models
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("secret")]
    public partial class secret
    {
           public secret(){


           }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int Id {get;set;}

           /// <summary>
           /// Desc:映射表Id
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? MapId {get;set;}

           /// <summary>
           /// Desc:秘密状态：1：加密；2：临时加密；3：解密；
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? SecretStatus {get;set;}

           /// <summary>
           /// Desc:加密key
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SecretEncode {get;set;}

           /// <summary>
           /// Desc:秘密类型：1：字段加密；2：表加密；
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? SecretType {get;set;}

           /// <summary>
           /// Desc:临时解密开始时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? StartTime {get;set;}

           /// <summary>
           /// Desc:临时解密结束时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? EndTime {get;set;}

           /// <summary>
           /// Desc:加密类型
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int SecretMode {get;set;}

    }
}
