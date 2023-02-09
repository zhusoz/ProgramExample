using Models;
using ProgramsNetCore.Models.Dto.DataCollectDto;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProgramsNetCore.IService
{
    public interface IDataMapService:IBaseService<datamap>
    {
        /// <summary>
        /// 分类分级管理
        /// </summary>
        /// <param name="IsAlreadyTag">是否已标记</param>
        /// <returns></returns>
        Task<List<GetClassifyManageDto>> GetDatamaps(bool IsAlreadyTag);


        /// <summary>
        /// 分类分级管理
        /// </summary>
        /// <param name="IsAlreadyTag">是否已标记</param>
        /// <returns></returns>
        Task<List<GetClassifyManageDto>> GetDatamaps(bool IsAlreadyTag, int pageIndex, int pageSize,RefAsync<int> total);
    }
}
