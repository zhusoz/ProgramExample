﻿using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Models
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("approvaltask")]
    public partial class approvaltask
    {
        public approvaltask()
        {


        }
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        /// <summary>
        /// Desc:用户名称
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? UserId { get; set; }

        /// <summary>
        /// Desc:起始时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// Desc:结束时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// Desc:手机号码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Phone { get; set; }

        /// <summary>
        /// Desc:活动流程
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? CurrentProcess { get; set; }

        /// <summary>
        /// Desc:任务状态[0:等待申请 1:审批中 2:审批完成]
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? Status { get; set; }

        /// <summary>
        /// Desc:关联表id
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? AssociativeTable { get; set; }

        /// <summary>
        /// Desc:附属信息
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string AttachedInfo { get; set; }

        /// <summary>
        /// Desc:任务类型[0:未申请 1:已申请]
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? TaskType { get; set; }

        /// <summary>
        /// Desc:映射关系
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? InfoRelation { get; set; }

        /// <summary>
        /// Desc:是否为转库申请审批流程
        /// Default:
        /// Nullable:True
        /// </summary>           
        public bool IsTransferLib { get; set; }
    }
}
