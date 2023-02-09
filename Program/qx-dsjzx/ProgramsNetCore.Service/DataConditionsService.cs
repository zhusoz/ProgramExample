using Models;
using ProgramsNetCore.IReposity;
using ProgramsNetCore.IService;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Service
{
    public class DataConditionsService : BaseService<dataconditions>, IDataConditionsService
    {
        private readonly IBaseRep<dataconditions> _dal;

        public DataConditionsService(IBaseRep<dataconditions> dal)
        {
            _dal = dal;
            base._dal= dal;
        }
    }
}
