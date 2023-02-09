using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using NPOI.HPSF;
using NPOI.HSSF.UserModel;
using NPOI.POIFS.Crypt.Dsig;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using ProblemDisposal.Common.Basic;
using ProgramsNetCore.Common;
using ProgramsNetCore.Common.Basic;
using ProgramsNetCore.Common.EncryptionToDecrypt;
using ProgramsNetCore.IService;
using ProgramsNetCore.Models.Dto;
using ProgramsNetCore.Models.Dto.DataMigration;
using ProgramsNetCore.Models.Dto.Test;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Printing;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using DbType = SqlSugar.DbType;
using System.Linq;

namespace ProgramsNetCore.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IDataTableService _dataService;
        private readonly IDataTrendService _dtService;

        public TestController(IDataTableService dataService,IDataTrendService dtService)
        {
            _dataService = dataService;
            _dtService = dtService;
        }

        /// <summary>
        /// 获取DataTable
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PageResult<string>> GetDataTable(PageDto dto)
        {
            string sql = "select * from secretmode";
            RefAsync<int> total = 0;
            var result = await _dataService.GetDataTable(sql, dto.PageIndex, dto.PageSize, total);
            return new PageResult<string>(result.ToString(), total);
        }

        /// <summary>
        /// 获取DataTable
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<AjaxResult<int>> GetTest()
        {
            return new AjaxResult<int>((int)Common.PublicEnum.AuthorizeType.S_Admin);
        }

        ///// <summary>
        ///// 解密
        ///// </summary>
        ///// <param name="file"></param>
        ///// <returns></returns>
        //[HttpGet]
        //public async Task<AjaxResult<string>> Decrypt(string key, string content)
        //{
        //    var result = DEncrypt.AesDecryptECB( content,key);
        //    return new AjaxResult<string>(result);
        //}
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="file"></param>
        ///// <returns></returns>
        //[HttpGet]
        //public async Task<AjaxResult<string>> Encrypt(string key, string content)
        //{
        //    var result = DEncrypt.AesEncryptECB(content, key);
        //    return new AjaxResult<string>(result);
        //}

        /// <summary>
        /// XOR加解密数据
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<bool>> XORDecryptData(XORDEnDto dto)
        {
            var result =await await Task.Factory.StartNew(async () =>
            {
                try
                {
                    //dto.DBHost=DEncrypt.AesDecrypt(dto.DBHost,DecryptKey);
                    //dto.UserName = DEncrypt.AesDecrypt(dto.UserName, DecryptKey);
                    //dto.Passwrod = DEncrypt.AesDecryptECB(dto.Passwrod, DecryptKey);
                    //dto.DataBase = DEncrypt.AesDecrypt(dto.DataBase, DecryptKey);
                    DbType myDbType = new DbType();
                    string strConn = "";
                    switch (dto.DBType)
                    {
                        case 0: myDbType = DbType.MySql; strConn = $" server={dto.DBHost};Database={dto.DataBase};Uid={dto.UserName};Pwd={dto.Passwrod};Port={dto.Port};AllowLoadLocalInfile=true;charset=utf8mb4;"; break;
                        case 1: myDbType = DbType.SqlServer; strConn = $"server={(dto.DBHost.Contains(',') ? dto.DBHost : dto.DBHost + "," + dto.Port)};uid={dto.UserName};pwd={dto.Passwrod};database={dto.DataBase}"; break;
                        case 7: myDbType = DbType.Oracle; strConn = $"Data Source={dto.DBHost}/orcl;User ID={dto.UserName};Password={dto.DataBase}"; break;
                        case 5: myDbType = DbType.Dm; strConn = $"Server={dto.DBHost}; User Id={dto.UserName}; PWD={dto.Passwrod};DATABASE={dto.DataBase}"; break;
                        case 6: myDbType = DbType.Kdbndp; strConn = $"Server={dto.DBHost};Port={dto.Port};UID={dto.UserName};PWD={dto.Passwrod};database={dto.DataBase}"; break;
                    }
                    SqlSugarClient db = new SqlSugarClient(new ConnectionConfig()
                    {
                        ConnectionString = strConn,//连接符字串
                        DbType = myDbType, //数据库类型
                        IsAutoCloseConnection = true //不设成true要手动close
                    });
                    db.Open();

                    foreach (var item in dto.TableInfo)
                    {
                        string sql = $" select count(*) from  {dto.DataBase}.{item.TableName} where 1=1  ";
                        var sqlcount = await db.Ado.GetScalarAsync(sql);
                        if (sqlcount != null)
                        {
                            var nowtable = $"{item.TableName}_{DateTime.Now.ToString("hhmmss")}";
                            sql = $" create table {dto.ToDataBase}.{nowtable}  like {dto.DataBase}.{item.TableName} ; alter table {dto.ToDataBase}.{nowtable} character set utf8mb4 COLLATE utf8mb4_general_ci ;";
                            await db.Ado.ExecuteCommandAsync(sql);
                            int intcount = sqlcount.ToInt32();
                            intcount = intcount % 1000000 == 0 ? intcount / 1000000 : intcount / 1000000 + 1;
                            for (int j = 1; j <=intcount ; j++)
                            {
                                sql = $" select * from  {dto.DataBase}.{item.TableName} where 1=1  ";
                                var dt = await db.SqlQueryable<DataTable>(sql).ToDataTablePageAsync(j, 1000000);
                                if (dt == null || dt.Rows.Count <= 0) break;
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    foreach (var s in item.FieldName)
                                    {
                                        if (dto.ActionType == 1)
                                            dt.Rows[i][s] = XORDEncrypt.encrypt(dt.Rows[i][s].ToString());
                                        else dt.Rows[i][s] = XORDEncrypt.decrypt(dt.Rows[i][s].ToString());
                                    }
                                }
                                db.Aop.OnLogExecuting = (sql, pars) =>
                                {
                                    Console.WriteLine(sql + "\r\n" + db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
                                    Console.WriteLine();
                                };
                                //string insertsql = $" insert into {dto.ToDataBase}.{nowtable}(name,src) values('{dt.Rows[1]["name"]}','{dt.Rows[1]["src"]}') ";
                                //await db.Ado.ExecuteCommandAsync(insertsql);

                                await db.Fastest<DataTable>().PageSize(1000000).SetCharacterSet(dto.CharacterSet).AS(dto.ToDataBase + "." + nowtable).BulkCopyAsync(dt);
                            }
                        }
                    }
                    db.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            });

            return new AjaxResult<bool>(result);
        }

        [HttpGet]
        public async Task<AjaxResult<string>> TestDataTrend()
        {
           var id=await _dtService.GetEntity(i => i.Id == 1);

            return new AjaxResult<string>(id.AffectedRows.ToString());
        }


    }
}
