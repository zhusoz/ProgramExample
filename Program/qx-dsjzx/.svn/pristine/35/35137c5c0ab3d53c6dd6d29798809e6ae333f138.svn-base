using System;
using System.Collections.Generic;
using System.Text;
using Models;
using ProgramsNetCore.IReposity;
using ProgramsNetCore.IService;

namespace ProgramsNetCore.Service
{
    public class OriRuleService : BaseService<conn_orirule_rule>, IOriRuleService
    {
        private readonly IBaseRep<conn_orirule_rule> _dal;

        public OriRuleService(IBaseRep<conn_orirule_rule> dal)
        {
            _dal = dal;
            base._dal = dal;
        }
    }
}
