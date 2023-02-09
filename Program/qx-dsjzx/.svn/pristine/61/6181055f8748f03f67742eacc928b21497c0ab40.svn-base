using Models;
using ProgramsNetCore.IReposity;
using ProgramsNetCore.IService;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Service
{
    public class DbLogService : BaseService<log_db>, IDbLogService
    {
        private readonly IBaseRep<log_db> _dal;

        public DbLogService(IBaseRep<log_db> dal)
        {
            _dal=dal;
            base._dal=dal;
        }
    }
}
