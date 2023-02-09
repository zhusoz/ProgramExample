using Models;
using ProgramsNetCore.IReposity;
using ProgramsNetCore.IService;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Service
{
    public class MapService : BaseService<datamap>, IMapService
    {
        private readonly IBaseRep<datamap> _dal;

        public MapService(IBaseRep<datamap> dal)
        {
            _dal = dal;
            base._dal = dal;
        }
    }
}
