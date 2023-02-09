using Models;
using ProgramsNetCore.IReposity;
using ProgramsNetCore.IService;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Service
{
    public class SecretChildService : BaseService<secret_child>, ISecretChildService
    {
        private readonly IBaseRep<secret_child> _dal;

        public SecretChildService(IBaseRep<secret_child> dal)
        {
            _dal = dal;
            base._dal = dal;
        }
    }
}
