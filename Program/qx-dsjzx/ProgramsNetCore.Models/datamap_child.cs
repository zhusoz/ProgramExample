using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Models
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("datamap_child")]
    public partial class datamap_child
    {
           public datamap_child(){


           }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int Id {get;set;}

           /// <summary>
           /// Desc:父Id
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? ParentId {get;set;}
           public int? UserId {get;set;}

           /// <summary>
           /// Desc:表名
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string Name {get;set;}

           /// <summary>
           /// Desc:创建时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? CreateTime {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string MappingTable {get;set;}
        /// <summary>
        /// 条件
        /// </summary>
        public string Conditions { get; set; }
        /// <summary>
        /// 中文条件
        /// </summary>
        public string CN_Conditions { get; set; }

    }
}
