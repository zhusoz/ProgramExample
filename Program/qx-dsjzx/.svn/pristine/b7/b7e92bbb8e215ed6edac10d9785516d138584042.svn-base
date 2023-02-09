using Models;
using ProgramsNetCore.Models.Dto.DataSecurity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProgramsNetCore.IReposity
{
    public interface IAuthorityRep
    {
        Task<List<ResultDataAuthorityDto>> GetDataAuthorityInfo<TEntity>(Expression<Func<authority,bool>> exb,Expression<Func<authority, TEntity>> exo) where TEntity :class,new();
    }
}
