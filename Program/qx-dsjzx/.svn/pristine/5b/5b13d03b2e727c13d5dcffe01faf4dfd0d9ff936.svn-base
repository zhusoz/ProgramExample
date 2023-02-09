using Models;
using ProgramsNetCore.Models.Dto.DataCollectDto;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProgramsNetCore.IReposity
{
    public interface IDataMapRep
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="isAlreadyTag">是否已标记</param>
        /// <returns></returns>
        Task<List<GetClassifyManageDto>> GetDatamaps(bool IsAlreadyTag);

        Task<List<GetClassifyManageDto>> GetDatamaps(bool IsAlreadyTag, int pageIndex, int pageSize, RefAsync<int> total);


    }
}
