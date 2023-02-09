using Models;
using ProgramsNetCore.IReposity;
using ProgramsNetCore.IService;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Service
{
    public class DataTrendService : BaseService<datatrend>, IDataTrendService
    {
        private readonly IBaseRep<datatrend> _dal;

        public DataTrendService(IBaseRep<datatrend> dal)
        {
            _dal = dal;
            base._dal = dal;
        }
    }
}
