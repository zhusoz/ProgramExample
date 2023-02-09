﻿using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Models
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("datamodel_publicsheet")]
    public partial class datamodel_publicsheet
    {
           public datamodel_publicsheet(){


           }
           /// <summary>
           /// Desc:模型中的主题库
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int Id {get;set;}

           /// <summary>
           /// Desc:主题库Id
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SheetId {get;set;}

           /// <summary>
           /// Desc:主题库名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SheetName {get;set;}

           /// <summary>
           /// Desc:主题库字段
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
