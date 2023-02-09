using Models;
using ProgramsNetCore.IReposity;
using ProgramsNetCore.IService;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Service
{
    public class ServiceApplicationTaskInfoService : BaseService<serviceapplicationtaskinfo>, IServiceApplicationTaskInfoService
    {
        private readonly IBaseRep<serviceapplicationtaskinfo> _baseRep;

        public ServiceApplicationTaskInfoService(IBaseRep<serviceapplicationtaskinfo> baseRep)
        {
            _baseRep = baseRep;
            _dal = _baseRep;
        }
    }
}
