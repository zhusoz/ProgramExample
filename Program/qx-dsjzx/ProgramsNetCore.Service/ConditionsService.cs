using Models;
using ProgramsNetCore.IReposity;
using ProgramsNetCore.IService;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Service
{
    public class ConditionsService : BaseService<conditions>, IConditionsService
    {
        private readonly IBaseRep<conditions> _dal;

        public ConditionsService(IBaseRep<conditions> dal)
        {
            _dal = dal;
            base._dal= dal;
        }
    }
}
