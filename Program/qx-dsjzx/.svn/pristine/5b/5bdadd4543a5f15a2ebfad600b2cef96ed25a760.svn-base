using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Models
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("datamodel_privatesheet")]
    public partial class datamodel_privatesheet
    {
           public datamodel_privatesheet(){


           }
           /// <summary>
           /// Desc:模型中的私有库
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int Id {get;set;}

           /// <summary>
           /// Desc:私有库Id
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SheetId {get;set;}

           /// <summary>
           /// Desc:私有库名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SheetName {get;set;}

           /// <summary>
           /// Desc:私有库字段
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SheetField {get;set;}

           /// <summary>
           /// Desc:创建时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? CreateTime {get;set;}

           /// <summary>
           /// Desc:模型Id或任务Id
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? ModelId {get;set;}

           /// <summary>
           /// Desc:类型；1：模型；2：任务
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? Type {get;set;}

           /// <summary>
           /// Desc:模式名
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string PatternName {get;set;}

    }
}
