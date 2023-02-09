using Models;
using ProgramsNetCore.IReposity;
using ProgramsNetCore.IService;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Service
{
    public class PublicTaskService : BaseService<public_task>, IPublicTaskService
    {
        private readonly IBaseRep<public_task> _dal;

        public PublicTaskService(IBaseRep<public_task> dal)
        {
            _dal = dal;
            base._dal = dal;
        }
    }
}
