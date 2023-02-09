using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Models
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("dataconditions")]
    public partial class dataconditions
    {
           public dataconditions(){


           }
           /// <summary>
           /// Desc:筛选条件表
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int Id {get;set;}

           /// <summary>
           /// Desc:字段名
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string FieldName {get;set;}

           /// <summary>
           /// Desc:条件
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Condition {get;set;}

           /// <summary>
           /// Desc:字段值
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string FieldValue {get;set;}

           /// <summary>
           /// Desc:字段类型
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string FieldType {get;set;}

           /// <summary>
           /// Desc:模型或者任务ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? ModelId {get;set;}

           /// <summary>
           /// Desc:1：模型；2：任务
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? Type {get;set;}

           /// <summary>
           /// Desc:表名
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string TableName {get;set;}

           /// <summary>
           /// Desc:模式名
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string PatternName {get;set;}

    }
}
