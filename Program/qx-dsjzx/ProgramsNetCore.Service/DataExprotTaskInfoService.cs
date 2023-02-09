using Models;
using ProgramsNetCore.IReposity;
using ProgramsNetCore.IService;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Service
{
    public class DataExprotTaskInfoService : BaseService<dataexprottaskinfo>, IDataExprotTaskInfoService
    {
        private readonly IBaseRep<dataexprottaskinfo> _dal;

        public DataExprotTaskInfoService(IBaseRep<dataexprottaskinfo> dal)
        {
            _dal = dal;
            base._dal = dal;
        }
    }
}
