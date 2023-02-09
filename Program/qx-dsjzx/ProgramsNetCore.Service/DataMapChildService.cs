using Models;
using ProgramsNetCore.IReposity;
using ProgramsNetCore.IService;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Service
{
    public class DataMapChildService : BaseService<datamap_child>, IDataMapChildService
    {
        private readonly IBaseRep<datamap_child> _dal;

        public DataMapChildService(IBaseRep<datamap_child> dal)
        {
            _dal = dal;
            base._dal = dal;
        }
    }
}
