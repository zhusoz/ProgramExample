using Models;
using ProgramsNetCore.IReposity;
using ProgramsNetCore.IService;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Service
{
    public class RuleService : BaseService<rule>, IRuleService
    {
        private readonly IBaseRep<rule> _dal;

        public RuleService(IBaseRep<rule> dal)
        {
            _dal = dal;
            base._dal=dal;
        }
    }
}
