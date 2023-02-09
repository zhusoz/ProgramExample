﻿using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Models
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("datamigrationtaskinfo")]
    public partial class datamigrationtaskinfo
    {
           public datamigrationtaskinfo(){


           }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int Id {get;set;}

           /// <summary>
           /// Desc:链接地址
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string HostLink {get;set;}

           /// <summary>
           /// Desc:数据库名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string DbName {get;set;}

           /// <summary>
           /// Desc:数据库密码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string DbPwd {get;set;}

           /// <summary>
           /// Desc:秘钥
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SecretKey {get;set;}

           /// <summary>
           /// Desc:目标数据库中文名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string TargetName_cn {get;set;}
           public string TargetName {get;set;}

           /// <summary>
           /// Desc:申请人
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? Applicant {get;set;}

           /// <summary>
           /// Desc:部门
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? Department {get;set;}

           /// <summary>
           /// Desc:手机号码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Phone {get;set;}

        /// <summary>
        /// Desc:导出类型 1：数据库；2：数据表；3：数据库（仅结构）；4：数据表（仅结构）
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int ExportType {get;set;}

        /// <summary>
        /// 修饰类型
        /// </summary>
        public int ModifierType { get; set; }

        public int IsImplement { get; set; }
        public int DbType { get; set; }
        public string Port { get; set; }


    }
}
