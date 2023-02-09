using Models;
using ProgramsNetCore.IReposity;
using ProgramsNetCore.IService;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Service
{
    public class TopologyService : BaseService<topology>, ITopologyService
    {
        private readonly IBaseRep<topology> _dal;

        public TopologyService(IBaseRep<topology> dal)
        {
            _dal = dal;
            base._dal= dal;
        }
    }
}
