using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Models
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("datamap")]
    public partial class datamap
    {
           public datamap(){


           }
           /// <summary>
           /// Desc:主键
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int Id {get;set;}

           /// <summary>
           /// Desc:英文名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string EnName {get;set;}

           /// <summary>
           /// Desc:模型名称
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string Name {get;set;}

           /// <summary>
           /// Desc:编号
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string GUID {get;set;}

           /// <summary>
           /// Desc:是否私有
           /// Default:
           /// Nullable:False
           /// </summary>           
           public bool IsPrivate {get;set;}

           /// <summary>
           /// Desc:归集人[如果为私有,则为私有库拥有者]
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? UserId {get;set;}

           /// <summary>
           /// Desc:私有库的过期时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? ExpireTime {get;set;}

           /// <summary>
           /// Desc:数源单位
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int Attribution {get;set;}

           /// <summary>
           /// Desc:分层类型
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int LayeredType {get;set;}

           /// <summary>
           /// Desc:修饰类型
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int ModifierType {get;set;}

           /// <summary>
           /// Desc:描述
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Describe {get;set;}

           /// <summary>
           /// Desc:信息摘要
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string InfoSummary {get;set;}

           /// <summary>
           /// Desc:创建时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? CreateTime {get;set;}

           /// <summary>
           /// Desc:更新时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? UpdateTime {get;set;}

           /// <summary>
           /// Desc:归集状态[0:未归集 1:已归集]
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? Status {get;set;}

           /// <summary>
           /// Desc:货架状态[0:上架 1:未上架 2:下架]
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? RackStatus {get;set;}

           /// <summary>
           /// Desc:申请数量
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? ApplicationNum {get;set;}

           /// <summary>
           /// Desc:访问量
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? Traffic {get;set;}

           /// <summary>
           /// Desc:关联表
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string AssociativeTable {get;set;}

           /// <summary>
           /// Desc:频率
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? Frequency {get;set;}

           /// <summary>
           /// Desc:数据来源
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Source {get;set;}

           /// <summary>
           /// Desc:所属系统来源
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ApplicationSystemName {get;set;}

           /// <summary>
           /// Desc:联系人
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string LinkPerson {get;set;}

           /// <summary>
           /// Desc:联系电话
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string LinkPhone {get;set;}

           /// <summary>
           /// Desc:是否允许导入数据
           /// Default:
           /// Nullable:False
           /// </summary>           
           public bool IsEnableImportData {get;set;}

           /// <summary>
           /// Desc:导入数据的时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? ImportDataTime {get;set;}

           /// <summary>
           /// Desc:是否允许分类分级
           /// Default:
           /// Nullable:False
           /// </summary>           
           public bool IsEnableClassify {get;set;}

           /// <summary>
           /// Desc:映射字表
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? MapChild {get;set;}

           /// <summary>
           /// Desc:是否映射
           /// Default:
           /// Nullable:True
           /// </summary>           
           public bool? IsMapping {get;set;}

           /// <summary>
           /// Desc:是否转数据服务
           /// Default:
           /// Nullable:True
           /// </summary>           
           public bool? IsDataService {get;set;}

           /// <summary>
           /// Desc:模式名
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string PatternName {get;set;}

    }
}
