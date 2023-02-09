using Models;
using ProgramsNetCore.IReposity;
using ProgramsNetCore.IService;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Service
{
    public class DataModelPublicSheetService : BaseService<datamodel_publicsheet>, IDataModelPublicSheetService
    { 
        private readonly IBaseRep<datamodel_publicsheet> _dal;

        public DataModelPublicSheetService(IBaseRep<datamodel_publicsheet> dal)
        {
            _dal = dal;
            base._dal= dal;
        }
    }
}
