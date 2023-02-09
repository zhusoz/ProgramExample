using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Models
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("cloud_user")]
    public partial class cloud_user
    {
           public cloud_user(){


           }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true)]
           public object id {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string account {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string password {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string real_name {get;set;}

           /// <summary>
           /// Desc:
           /// Default:CURRENT_TIMESTAMP
           /// Nullable:True
           /// </summary>           
           public DateTime? birthday {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string address {get;set;}

           /// <summary>
           /// Desc:0表示女,1表示男
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public int sex {get;set;}

           /// <summary>
           /// Desc:0表示该用户处于可用状态,1表示该用户不可用.
           /// Default:0
           /// Nullable:True
           /// </summary>           
           public int? is_enabled {get;set;}

           /// <summary>
           /// Desc:0表示该用户处于正常状态,1表示该用户处于锁定状态.
           /// Default:0
           /// Nullable:True
           /// </summary>           
           public int? is_locked {get;set;}

           /// <summary>
           /// Desc:0表示正常,1表示逻辑删除.
           /// Default:0
           /// Nullable:True
           /// </summary>           
           public int? is_deleted {get;set;}

           /// <summary>
           /// Desc:描述信息 已经改为监督系统的部门id字段，请勿删除
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int description {get;set;}

           /// <summary>
           /// Desc:创建时间
           /// Default:CURRENT_TIMESTAMP
           /// Nullable:True
           /// </summary>           
           public DateTime? gmt_create {get;set;}

           /// <summary>
           /// Desc:职务
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string job {get;set;}

           /// <summary>
           /// Desc:身份证
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string id_card {get;set;}

           /// <summary>
           /// Desc:单位
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string company {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string path {get;set;}

           /// <summary>
           /// Desc:用户类型0代表平台管理用户
           /// Default:0
           /// Nullable:True
           /// </summary>           
           public int? type {get;set;}

           /// <summary>
           /// Desc:手机号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string phone { get; set; }


    }
}
