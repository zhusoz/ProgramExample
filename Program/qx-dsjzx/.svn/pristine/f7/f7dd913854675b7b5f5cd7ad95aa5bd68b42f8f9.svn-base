using Models;
using ProgramsNetCore.IReposity;
using ProgramsNetCore.IService;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Service
{
    public class DataModelPrivateSheetService : BaseService<datamodel_privatesheet>, IDataModelPrivateSheetService
    {
        private readonly IBaseRep<datamodel_privatesheet> _dal;

        public DataModelPrivateSheetService(IBaseRep<datamodel_privatesheet> dal)
        {
            _dal = dal;
            base._dal= dal;
        }
    }
}
