using Models;
using ProgramsNetCore.IReposity;
using ProgramsNetCore.IService;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Service
{
    public class OrderProcessInfoService : BaseService<order_processinfo>, IOrderProcessInfoService
    {
        private readonly IBaseRep<order_processinfo> _dal;

        public OrderProcessInfoService(IBaseRep<order_processinfo> dal)
        {
            _dal = dal;
            base._dal = dal;
        }
    }
}
