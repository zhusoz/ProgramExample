using Models;
using ProgramsNetCore.IReposity;
using ProgramsNetCore.IService;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Service
{
    public class AttributionService:BaseService<attribution>, IAttributionService
    {
        private readonly IBaseRep<attribution> _dal;

        public AttributionService(IBaseRep<attribution> dal)
        {
            _dal=dal;
            base._dal=dal;
        }

    }
}
