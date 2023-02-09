using Models;
using ProgramsNetCore.IReposity;
using ProgramsNetCore.IService;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Service
{
    public class UserService : BaseService<cloud_user>, IUserService
    {
        private readonly IBaseRep<cloud_user> _dal;

        public UserService(IBaseRep<cloud_user> dal)
        {
            _dal = dal;
            base._dal = dal;
        }

    }
}
