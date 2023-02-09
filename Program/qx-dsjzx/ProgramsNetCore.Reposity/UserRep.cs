using Models;
using ProgramsNetCore.IReposity;
using ProgramsNetCore.Models;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Reposity
{
    public class UserRep:BaseRep<cloud_user>,IUserRep
    {
        public SqlSugarScope db = BaseDB.Db;


    }
}
