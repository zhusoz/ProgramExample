using Models;
using ProgramsNetCore.IReposity;
using ProgramsNetCore.IService;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Service
{
    public class DataServiceTaskInfoService:BaseService<dataservicetaskinfo>,IDataServiceTaskInfoService
    {
        private readonly IBaseRep<dataservicetaskinfo> _baseRep;

        public DataServiceTaskInfoService(IBaseRep<dataservicetaskinfo> baseRep)
        {
            _baseRep = baseRep;
            _dal = _baseRep;
        }
    }
}
