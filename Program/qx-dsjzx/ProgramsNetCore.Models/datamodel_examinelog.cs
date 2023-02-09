using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Models
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("datamodel_examinelog")]
    public partial class datamodel_examinelog
    {
           public datamodel_examinelog(){


           }
           /// <summary>
           /// Desc:模型审核历史
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int Id {get;set;}

           /// <summary>
           /// Desc:模型Id
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? ModelId {get;set;}

           /// <summary>
           /// Desc:模型名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ModelName {get;set;}

           /// <summary>
           /// Desc:建模单位
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Tissue {get;set;}

           /// <summary>
           /// Desc:所属领域
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Realm {get;set;}

           /// <summary>
           /// Desc:所属区域
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Region {get;set;}

           /// <summary>
           /// Desc:模型类型
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Type {get;set;}

           /// <summary>
           /// Desc:操作人
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Operator {get;set;}

           /// <summary>
           /// Desc:操作人ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? OperatorId {get;set;}

           /// <summary>
           /// Desc:操作时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? OperationTime {get;set;}

           /// <summary>
           /// Desc:操作
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Operation {get;set;}

    }
}
