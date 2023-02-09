using Models;
using ProgramsNetCore.IReposity;
using ProgramsNetCore.IService;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Service
{
    public class PermissionService : BaseService<cloud_permission>, IPermissionService
    {
        private readonly IBaseRep<cloud_permission> _dal;

        public PermissionService(IBaseRep<cloud_permission> dal)
        {
            _dal = dal;
            base._dal = dal;
        }
    }
}
