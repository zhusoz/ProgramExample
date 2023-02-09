using Models;
using ProgramsNetCore.IReposity;
using ProgramsNetCore.IService;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Service
{
    public class OrgService:BaseService<cloud_org>,IOrgService
    {
        private readonly IBaseRep<cloud_org> _dal;
        public OrgService(IBaseRep<cloud_org> dal)
        {
            _dal=dal;
            base._dal=dal;
        }
    }
}
