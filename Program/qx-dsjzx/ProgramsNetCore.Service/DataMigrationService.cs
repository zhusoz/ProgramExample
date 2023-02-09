using Models;
using ProgramsNetCore.IReposity;
using ProgramsNetCore.IService;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Service
{
    public class DataMigrationService : BaseService<datamigrationtaskinfo>, IDataMigrationService
    {
        private readonly IBaseRep<datamigrationtaskinfo> _dal;

        public DataMigrationService(IBaseRep<datamigrationtaskinfo> dal)
        {
            _dal = dal;
            base._dal = dal;
        }
    }
}
