﻿using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Models
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("serviceapplicationtaskinfo")]
    public partial class serviceapplicationtaskinfo
    {
           public serviceapplicationtaskinfo(){


           }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int Id {get;set;}

           /// <summary>
           /// Desc:申请人
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? Applicant {get;set;}

           /// <summary>
           /// Desc:审批人
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? Approvers {get;set;}

           /// <summary>
           /// Desc:创建时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? CreateTime {get;set;}

           /// <summary>
           /// Desc:角色所在组
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? GroupId {get;set;}

           /// <summary>
           /// Desc:数源单位
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? Attribution {get;set;}

           /// <summary>
           /// Desc:任务Id
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? TaskId {get;set;}

           /// <summary>
           /// Desc:权限Id
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? AuthorityId {get;set;}

           /// <summary>
           /// Desc:状态 0 待授权 1 已授权 2 已驳回
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? Status {get;set;}

    }
}
