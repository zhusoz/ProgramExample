using Models;
using ProgramsNetCore.IReposity;
using ProgramsNetCore.IService;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Service
{
    public class AuthoriyService : BaseService<authority>, IAuthoriyService
    {
        private readonly IBaseRep<authority> _rep;

        public AuthoriyService(IBaseRep<authority> rep)
        {
            _rep = rep;
            base._dal=rep;
        }
    }
}
