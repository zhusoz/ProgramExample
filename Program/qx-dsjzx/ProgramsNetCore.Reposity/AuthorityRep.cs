using Models;
using ProgramsNetCore.IReposity;
using ProgramsNetCore.Models.Dto.DataSecurity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;
using ProgramsNetCore.Models;

namespace ProgramsNetCore.Reposity
{
    public class AuthorityRep : IAuthorityRep
    {

        SqlSugarClient db = BaseDB.GetClient();

        public async Task<List<ResultDataAuthorityDto>> GetDataAuthorityInfo<TEntity>(Expression<Func<authority, bool>> exb, Expression<Func<authority, TEntity>> exo) where TEntity : class, new()
        {
            //var result = await db.Queryable<authority>().Where(exb).Mapper((a, b) =>
            //{

            //}).Select(exo).ToListAsync();
            return null;
        }
    }
}
