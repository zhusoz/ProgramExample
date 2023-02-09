using Models;
using ProgramsNetCore.IReposity;
using ProgramsNetCore.IService;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Service
{
    public class SecretModeService : BaseService<secretmode>, ISecretModeService
    {
        private readonly IBaseRep<secretmode> _dal;

        public SecretModeService(IBaseRep<secretmode> dal)
        {
            _dal = dal;
            base._dal = dal;
        }
    }
}
