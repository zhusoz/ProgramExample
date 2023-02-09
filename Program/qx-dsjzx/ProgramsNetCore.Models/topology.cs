﻿using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Models
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("topology")]
    public partial class topology
    {
           public topology(){


           }
           /// <summary>
           /// Desc:数据碰撞拓扑结构表
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int Id {get;set;}

           /// <summary>
           /// Desc:类型；1：模型；2：任务；
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? Type {get;set;}

           /// <summary>
           /// Desc:模型或者任务ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? ModelId {get;set;}

           /// <summary>
           /// Desc:拓扑图上的元素ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string TopologyId {get;set;}

           /// <summary>
           /// Desc:拓扑图上的元素类型
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string TopologyType {get;set;}

           /// <summary>
           /// Desc:拓扑图上元素的标签
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string TopologyLabel {get;set;}

           /// <summary>
           /// Desc:X坐标
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? X {get;set;}

           /// <summary>
           /// Desc:Y坐标
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? Y {get;set;}

           /// <summary>
           /// Desc:元素对应的表名
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string TableName {get;set;}

           /// <summary>
           /// Desc:该元素相连接的元素ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? TopologyConnectId {get;set;}

           /// <summary>
           /// Desc:该元素相连接的元素所绑定的表名
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string TableNameB {get;set;}

           /// <summary>
           /// Desc:模式名
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string PatternName {get;set;}

    }
}
