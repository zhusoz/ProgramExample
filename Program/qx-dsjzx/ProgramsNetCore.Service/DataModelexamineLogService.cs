using Models;
using ProgramsNetCore.IReposity;
using ProgramsNetCore.IService;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Service
{
    public class DataModelexamineLogService : BaseService<datamodel_examinelog>, IDataModelExamineLogService
    {
        private readonly IBaseRep<datamodel_examinelog> _dal;

        public DataModelexamineLogService(IBaseRep<datamodel_examinelog> dal)
        {
            _dal = dal;
            base._dal= dal;
        }
    }
}
