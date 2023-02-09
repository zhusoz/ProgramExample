using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Models
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("process")]
    public partial class process
    {
        public process()
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
        /// Desc:用户
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? UserId { get; set; }

        /// <summary>
        /// Desc:任务Id
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? TaskId { get; set; }

        /// <summary>
        /// Desc:描述
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Describe { get; set; }

        /// <summary>
        /// Desc:附件
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Accessory { get; set; }

        /// <summary>
        /// Desc:状态：0:未审批；1:审批通过；2:驳回
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? Status { get; set; }

        /// <summary>
        /// Desc:执行时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? CreateTime { get; set; }


        /// <summary>
        /// Desc:流程类型Id[0:归集申请审批 1:数据导入审批 2:分类分级审批]
        /// Default:
        /// Nullable:True
        /// </summary>
        public int? ProcessType { get; set; }

        /// <summary>
        /// Desc:审批流程号Id——approvaltask
        /// Default:
        /// Nullable:True
        /// </summary>
        public int? LinkApproval { get; set; }
    }
}
