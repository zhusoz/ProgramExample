using Models;
using ProgramsNetCore.IReposity;
using ProgramsNetCore.IService;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Service
{
    public class DataTaskService : BaseService<datatask>, IDataTaskService
    {
        private readonly IBaseRep<datatask> _dal;

        public DataTaskService(IBaseRep<datatask> dal)
        {
            _dal = dal;
            base._dal= dal;
        }
    }
}
