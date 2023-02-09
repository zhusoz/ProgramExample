using Models;
using ProgramsNetCore.IReposity;
using ProgramsNetCore.IService;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Service
{
    public class DataModelService : BaseService<datamodel>,     IDataModelService
    {
        private readonly IBaseRep<datamodel> _dal;

        public DataModelService(IBaseRep<datamodel> dal)
        {
            _dal = dal;
            base._dal= dal;
        }
    }
}
