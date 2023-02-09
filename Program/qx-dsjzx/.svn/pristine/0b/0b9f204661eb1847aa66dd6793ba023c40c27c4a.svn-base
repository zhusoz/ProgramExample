using Models;
using ProgramsNetCore.IReposity;
using ProgramsNetCore.IService;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Service
{
    public class RepairOrderService : BaseService<repair_order>,IRepairOrderService
    {
        private readonly IBaseRep<repair_order> _dal;

        public RepairOrderService(IBaseRep<repair_order> dal)
        {
            _dal = dal;
            base._dal = dal;
        }
    }
}
