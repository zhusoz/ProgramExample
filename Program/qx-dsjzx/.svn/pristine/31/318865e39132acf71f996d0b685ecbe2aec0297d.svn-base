using Models;
using ProgramsNetCore.IReposity;
using ProgramsNetCore.IService;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Service
{
    public class GroupService:BaseService<cloud_role_group>,IGroupService
    {
        private readonly IBaseRep<cloud_role_group> _dal;

        public GroupService(IBaseRep<cloud_role_group> dal)
        {
            _dal=dal;
            base._dal=dal;
        }


    }
}
