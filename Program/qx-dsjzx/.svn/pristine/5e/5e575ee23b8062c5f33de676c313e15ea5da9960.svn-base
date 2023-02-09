using Models;
using ProgramsNetCore.IReposity;
using ProgramsNetCore.IService;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Service
{
    public class LayeredService:BaseService<layered>, ILayeredService
    {
        private readonly IBaseRep<layered> _dal;

        public LayeredService(IBaseRep<layered> dal)
        {
            _dal=dal;
            base._dal=dal;
        }
    }
}
