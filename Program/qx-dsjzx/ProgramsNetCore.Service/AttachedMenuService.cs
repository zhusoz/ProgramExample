using Models;
using ProgramsNetCore.IReposity;
using ProgramsNetCore.IService;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Service
{
    public class AttachedMenuService : BaseService<attachedmenu>, IAttachedMenuService
    {
        private readonly IBaseRep<attachedmenu> _dal;

        public AttachedMenuService(IBaseRep<attachedmenu> dal)
        {
            _dal = dal;
            base._dal = dal;
        }
    }
}
