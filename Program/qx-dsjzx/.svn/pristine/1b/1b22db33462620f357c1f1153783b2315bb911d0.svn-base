using Models;
using ProgramsNetCore.IReposity;
using ProgramsNetCore.IService;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Service
{
    public class PlatformLogService : BaseService<log_platform>, IPlatformLogService
    {
        private readonly IBaseRep<log_platform> _dal;

        public PlatformLogService(IBaseRep<log_platform> dal)
        {
            _dal= dal;
            base._dal=dal;
        }

    }
}
