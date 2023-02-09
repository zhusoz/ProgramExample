using Models;
using ProgramsNetCore.IReposity;
using ProgramsNetCore.IService;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Service
{
    public class ProcessService : BaseService<process>, IProcessService
    {
        private readonly IBaseRep<process> _dal;

        public ProcessService(IBaseRep<process> dal)
        {
            _dal = dal;
            base._dal = dal;
        }
    }
}
