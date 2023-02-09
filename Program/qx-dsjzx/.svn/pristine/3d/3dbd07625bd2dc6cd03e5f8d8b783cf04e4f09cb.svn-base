using System;
using System.Collections.Generic;
using System.Text;
using Models;
using ProgramsNetCore.IReposity;
using ProgramsNetCore.IService;

namespace ProgramsNetCore.Service
{
    public class DataMigrationChildValueService : BaseService<datamigrationtaskinfochildvalue>, IDataMigrationChildValueService
    {
        private readonly IBaseRep<datamigrationtaskinfochildvalue> _dal;

        public DataMigrationChildValueService(IBaseRep<datamigrationtaskinfochildvalue> dal)
        {
            _dal = dal;
            base._dal = dal;
        }
    }
}
