using Models;
using ProgramsNetCore.IReposity;
using ProgramsNetCore.IService;
using ProgramsNetCore.Models.Dto.DataCollectDto;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProgramsNetCore.Service
{
    public class DataMapService : BaseService<datamap>, IDataMapService
    {
        private readonly IBaseRep<datamap> _dal;
        private readonly IDataMapRep _dataMapRep;

        public DataMapService(IBaseRep<datamap> dal, IDataMapRep dataMapRep)
        {
            _dal = dal;
            _dataMapRep=dataMapRep;
            base._dal= dal;
        }

        public Task<List<GetClassifyManageDto>> GetDatamaps(bool IsAlreadyTag)
        {
            return _dataMapRep.GetDatamaps(IsAlreadyTag);
        }

        public Task<List<GetClassifyManageDto>> GetDatamaps(bool IsAlreadyTag, int pageIndex, int pageSize, RefAsync<int> total)
        {
            return _dataMapRep.GetDatamaps(IsAlreadyTag, pageIndex, pageSize, total);
        }
    }
}
