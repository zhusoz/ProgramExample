using Models;
using Org.BouncyCastle.Crypto.Prng.Drbg;
using ProgramsNetCore.IReposity;
using ProgramsNetCore.IService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProgramsNetCore.Service
{
    public class DataServiceJoinService : IDataServiceJoinService
    {
        private readonly IDateServiceRep _rep;

        public DataServiceJoinService(IDateServiceRep rep)
        {
            _rep = rep;
        }

        public async Task<datamap> GetDatamap(int taskId)
        {
            return await _rep.GetDataMap(taskId);
        }
    }
}
