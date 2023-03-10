using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Models.Dto.HomeDto
{

    /// <summary>
    /// 首页总体运行情况dto
    /// </summary>
    public class GetHomeResultDto
    {
        /// <summary>
        /// 主库是否在线
        /// </summary>
        public bool IsMainDbOnline { get; set; }

        /// <summary>
        /// 从库是否在线
        /// </summary>
        public bool IsAffiliatedDbOnline { get; set; }

        /// <summary>
        /// 主库总容量
        /// </summary>
        public int MainDbTotalVolume { get; set; }

        /// <summary>
        /// 主库剩余容量
        /// </summary>
        public int MainDbFreeVolume { get; set; }

        /// <summary>
        /// 从库总容量(TB)
        /// </summary>
        public int AffiliatedDbTotalVolume { get; set; }

        /// <summary>
        /// 从库总容量(TB)
        /// </summary>
        public int AffiliatedDbFreeVolume { get; set; }

        /// <summary>
        /// 数据汇聚数据总量(万条)
        /// </summary>
        public int DataCollectTotalCount { get; set; }

        /// <summary>
        /// 数据汇聚内部数据总量(万条)
        /// </summary>
        public int DataCollectInternalCount { get; set; }

        /// <summary>
        /// 数据汇聚外部数据总量(万条)
        /// </summary>
        public int DataCollectExternalCount { get; set; }

        /// <summary>
        /// 数据动态交换总量(万条)
        /// </summary>
        public int DataTrendTotalCount { get; set; }

        /// <summary>
        /// 数据动态流入数据总量(万条)
        /// </summary>
        public int DataTrendInternalCount { get; set; }

        /// <summary>
        /// 数据动态流出数据总量(万条)
        /// </summary>
        public int DataTrendExternalCount { get; set; }

        //↓↓↓↓↓↓↓↓↓↓↓数据平台↓↓↓↓↓↓↓↓↓↓↓↓↓

        /// <summary>
        /// 单位总数
        /// </summary>
        public int UnitsCount { get; set; }

        /// <summary>
        /// 数据表总数
        /// </summary>
        public int DataTablesCount { get; set; }

        /// <summary>
        /// 数据项数
        /// </summary>
        public int DataItemsCount { get; set; }

        /// <summary>
        /// 规则总数
        /// </summary>
        public int RulesCount { get; set; }
    }


    /// <summary>
    /// 首页总体运行情况dto
    /// </summary>
    public class HomeResultDto
    {
        /// <summary>
        /// 数据库健康状况
        /// </summary>
        public DbHealth DbHealthProp { get; set; }

        /// <summary>
        /// 数据库主库容量
        /// </summary>
        public DbVolume MainDbVolumeProp { get; set; }

        /// <summary>
        /// 数据库从库容量
        /// </summary>
        public DbVolume AffiliatedDbVolumeProp { get; set; }

        /// <summary>
        /// 数据汇聚
        /// </summary>
        public DataConverge DataConvergeProp { get; set; }

        /// <summary>
        /// 数据动态
        /// </summary>
        public DataTrend DataTrendProp { get; set; }

        /// <summary>
        /// 数据平台
        /// </summary>
        public DataPlatform DataPlatformProp { get; set; }
    }


    /// <summary>
    /// 数据库健康情况
    /// </summary>
    public class DbHealth
    {
        /// <summary>
        /// 主库是否在线
        /// </summary>
        public bool IsMainDbOnline { get; set; }

        /// <summary>
        /// 从库是否在线
        /// </summary>
        public bool IsAffiliatedDbOnline { get; set; }

    }

    /// <summary>
    /// 数据库容量[单位:TB]
    /// </summary>
    public class DbVolume
    {
        /// <summary>
        /// 总容量
        /// </summary>
        public double TotalVolume { get; set; }

        /// <summary>
        /// 余量
        /// </summary>
        public double FreeVolume { get; set; }


    }

    /// <summary>
    /// 数据汇聚
    /// </summary>
    public class DataConverge
    {
        /// <summary>
        /// 数据总量(万条)
        /// </summary>
        public double TotalCount { get; set; }

        /// <summary>
        /// 内部数据总量(万条)
        /// </summary>
        public double InternalCount { get; set; }

        /// <summary>
        /// 外部数据总量(万条)
        /// </summary>
        public double ExternalCount { get; set; }

        /// <summary>
        /// 每日新增(条)
        /// </summary>
        public double IncreasedPerDay { get; set; }
    }

    /// <summary>
    /// 数据动态
    /// </summary>
    public class DataTrend
    {
        /// <summary>
        /// 交换总量
        /// </summary>
        public double TotalCount { get; set; }

        /// <summary>
        /// 流入数据总量(万条)
        /// </summary>
        public double InCount { get; set; }

        /// <summary>
        /// 流出数据总量(万条)
        /// </summary>
        public double OutCount { get; set; }

    }

    /// <summary>
    /// 数据平台
    /// </summary>
    public class DataPlatform
    {
        /// <summary>
        /// 单元数量
        /// </summary>
        public int UnitsCount { get; set; }

        /// <summary>
        /// 数据表数量
        /// </summary>
        public int DataTablesCount { get; set; }

        /// <summary>
        /// 数据项数量
        /// </summary>
        public int DataItemsCount { get; set; }

        /// <summary>
        /// 规则数量
        /// </summary>
        public int RulesCount { get; set; }
    }


}
