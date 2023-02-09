using Models;
using ProgramsNetCore.IReposity;
using ProgramsNetCore.IService;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Service
{
    public class ErrorLogService : BaseService<log_error>, IErrorLogService
    {
        private readonly IBaseRep<log_error> _dal;

        public ErrorLogService(IBaseRep<log_error> dal)
        {
            _dal=dal;
            base._dal=dal;
        }

    }
}
