using Models;
using ProgramsNetCore.IReposity;
using ProgramsNetCore.IService;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Service
{
    public class TableToTableService : BaseService<tabletotable>, ITableToTableService
    {
        private readonly IBaseRep<tabletotable> _dal;

        public TableToTableService(IBaseRep<tabletotable> dal)
        {
            _dal = dal;
            base._dal= dal;
        }
    }
}
