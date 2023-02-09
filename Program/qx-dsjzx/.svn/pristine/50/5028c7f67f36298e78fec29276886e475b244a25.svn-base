using Models;
using ProgramsNetCore.IReposity;
using ProgramsNetCore.IService;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Service
{
    public class SecretService : BaseService<secret>, ISecretService
    {
        private readonly IBaseRep<secret> _dal;

        public SecretService(IBaseRep<secret> dal)
        {
            _dal = dal;
            base._dal = dal;
        }
    }
}
