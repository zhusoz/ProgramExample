using Models;
using ProgramsNetCore.IReposity;
using ProgramsNetCore.Models;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProgramsNetCore.Reposity
{
    public class DateServiceRep: IDateServiceRep
    {
        public SqlSugarScope db = BaseDB.Db;

        public async Task<datamap> GetDataMap(int taskId)
        {
            return await db.Queryable<datamap>()
                .InnerJoin<dataservicetaskinfo>((d, ds) => d.Id == ds.AssociativeTable)
                .InnerJoin<public_task>((d, ds, pt) => ds.Id == pt.InfoRelation)
                .Where((d, ds, pt) => pt.Id == taskId)
                .Select((d, ds, pt) => new datamap { Id = d.Id, Name = d.Name, Attribution = d.Attribution })
                .FirstAsync();
        }
    }
}
