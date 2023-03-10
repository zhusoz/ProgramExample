using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using ProblemDisposal.Common.Basic;
using ProgramsNetCore.Common;
using ProgramsNetCore.Common.Basic;
using ProgramsNetCore.IService;
using ProgramsNetCore.Models.Dto;
using ProgramsNetCore.Models.Dto.DataMigration;
using ProgramsNetCore.Models.Dto.DataSecurity;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataSharing.Controllers
{
    /// <summary>
    /// 数据安全管理模块
    /// </summary>
    [Route("")]
    [ApiController]
    public class DataSecurityController : ControllerBase
    {
        private readonly IDataTableService _dtService;
        private readonly IAuthoriyService _authoriyService;
        private readonly ISecretModeService _smService;
        private readonly IMapper _map;
        private readonly ISecretService _secretService;
        private readonly IMapService _mapService;
        private readonly ISecretChildService _scService;

        public DataSecurityController(IDataTableService dtService,IAuthoriyService authoriyService,ISecretModeService smService,IMapper map,ISecretService secretService,IMapService mapService,ISecretChildService scService)
        {
            _dtService = dtService;
            _authoriyService = authoriyService;
            _smService = smService;
            _map = map;
            _secretService = secretService;
            _mapService = mapService;
            _scService = scService;
        }

        /// <summary>
        /// 分页获取数据安全管理模块列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]       
        public async Task<PageResult<List<ResultDataInfoDto>>> GetDataSecurityPage(DataInfoDto dto)
        {
            try
            {
                string sql = "select dt.id,dt.`Name`,dt.GUID,cu.real_name CollectionUser,co.Title CollectionDept,dt.CreateTime from  datamap dt left join  cloud_user cu on cu.id=dt.userid left join cloud_org co on co.Id=cu.description left join layered l on l.Id=dt.LayeredType  left join modified m on m.id=dt.ModifierType where 1=1  and  !isprivate";
                string sqlwhere = "";
                
                if (dto.Type == 1)
                {
                    sql = $"select dt.id,dt.`Name`,dt.GUID,cu.real_name CollectionUser,co.Title CollectionDept,dt.CreateTime from  datamap dt left join  cloud_user cu on cu.id=dt.userid left join cloud_org co on co.Id=cu.description left join layered l on l.Id=dt.LayeredType  left join modified m on m.id=dt.ModifierType  where 1=1 and (co.id={dto.DepartmentId} or co.Path like '%{dto.DepartmentId},%' ) and  !isprivate ";
                }else if (dto.Type == 3)
                {
                    sqlwhere = " and dt.id in  (select distinct mapid from secret ) ";
                }
                else if (dto.Type == 2)
                {
                    sqlwhere = " and dt.id not in   (select distinct mapid from secret ) ";
                }
                sql += sqlwhere;
                if (!string.IsNullOrEmpty(dto.LikeStr)) sql += $" and  dt.name like '%{dto.LikeStr}%' ";
                RefAsync<int> total = 0;
                sql += sqlwhere;
                var result = await _dtService.GetEntityPageList<ResultDataInfoDto>(sql, dto.PageIndex, dto.PageSize, total);
                //await Logger.AddPlatformLog("分页获取数据安全管理模块列表", LogType.DataAccess);
                return new PageResult<List<ResultDataInfoDto>>(result, total);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new PageResult<List<ResultDataInfoDto>>(null, 0);
            }
        }

        /// <summary>
        /// 获取当前数据表权限列表
        /// </summary>
        /// <param name="dataMapId">数据Id</param>
        /// <param name="type">权限类型：1：用户；2：单位</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<AjaxResult<List<ResultDataAuthorityDto>>> GetAuthorityInfo(int dataMapId,int type)
        {
            string sql = "";
            if (type == 1) {
            sql= $" select au.Role,RelevanceId objid,cu.real_name objname,au.starttime,au.endtime from authority au join cloud_user cu on cu.id=au.RelevanceId where au.type=1  and au.AssociativeTable={dataMapId} ";
               
            } else
            {
                sql = $" select au.Role,RelevanceId objid,co.title objname,au.starttime,au.endtime from authority au join cloud_org co on co.id=au.RelevanceId where au.type=2 and au.AssociativeTable={dataMapId}  ";
            }
            var result = await _dtService.GetEntityList<ResultDataAuthorityDto>(sql);
            //await Logger.AddPlatformLog($"获取数据表:{dataMapId}权限类型为{(type == 1 ? "用户" : "单位")}列表", LogType.DataAccess);
            return new AjaxResult<List<ResultDataAuthorityDto>>(result);
        }
        /// <summary>
        /// [8.11更新]增加数据表权限
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<string>> SettingAuthority(AddAuthorityDto dto)
        {
            if (dto.Info == null) return new AjaxResult<string>(false, "数据错误！");
            dto.Info.Select(i => new authority { AssociativeTable = dto.DataId, RelevanceId = i.ObjId, Type = dto.AuthorityType, Role = i.RoleId, StartTime = DateTime.Now, EndTime = i.EndTime }).ToList().ForEach(async i => { if ((await _authoriyService.GetEntity(e => e.AssociativeTable == i.AssociativeTable && i.RelevanceId == e.RelevanceId && i.Type == e.Type)) == null) { var result = await _authoriyService.Add(i); } });
            //await Logger.AddPlatformLog($"增加数据表:{dto.DataId}权限,类型为{(dto.AuthorityType == 1 ? "用户" : "部门")}", LogType.DataOperation);
            return new AjaxResult<string>();
        }
        /// <summary>
        /// [8.11增加]修改权限
        /// </summary>
        /// <param name="Dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<string>> UpdateAuthority(UpdateAuthorityDto Dto)
        {
            var authorizeentity = await _authoriyService.GetEntity(i => i.RelevanceId == Dto.UserId&&i.AssociativeTable==Dto.DataId);
            if (authorizeentity == null) return new AjaxResult<string>(false, "数据错误！");
            var result = await _authoriyService.UpdateColumn(new authority { Id = authorizeentity.Id, EndTime = Dto.EndTime }, i => new { i.Id, i.EndTime });
            //await Logger.AddPlatformLog($"修改权限表:{authorizeentity.Id}的权限", LogType.DataOperation);
            return new AjaxResult<string>(result > 0);
        }

        /// <summary>
        /// 获取角色信息：1：用户；2：部门
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<AjaxResult<List<ResultCompleteInfoDto>>> GetRoleInfo(int type)
        {
            string sql = "";
            if (type == 2) {

                sql = $" select cr.id roleid,cr.name rolename ,cur.userid userid ,cu.title username from cloud_role cr LEFT JOIN (select * from  cloud_user_role where type={type})   cur on cur.RoleId=cr.Id LEFT JOIN cloud_org cu on cu.id=cur.UserId where 1=1 ";

            }
            else
            {
                 sql = $" select cr.id roleid,cr.name rolename ,cur.userid userid,cu.real_name username from cloud_role cr LEFT JOIN (select * from  cloud_user_role where type={type})    cur on cur.RoleId=cr.Id LEFT JOIN cloud_user cu on cu.id=cur.UserId where 1=1 ";
            }
            RefAsync<int> total = 0;
            var result = await _dtService.GetEntityList<ResultCompleteInfoDto>(sql);
           
          var resultData= await  Task.Factory.StartNew(() => {
              var fresult= result.Select(i => new {i.RoleId,i.RoleName }).Distinct().Select(i=> new ResultCompleteInfoDto { RoleId=i.RoleId, RoleName=i.RoleName }).ToList();
                fresult.ForEach(i => {
                    i.Child = result.Where(j => j.RoleId == i.RoleId&&j.UserId!=null&&j.UserName!=null).ToList();
                });
                return fresult;
            });
            //    var resultdata = result.GroupBy(x =>new ResultDeptInfoDto { RoleId=x.RoleId, RoleName=x.RoleName }).Select(group => new ResultRoleInfoDto {   RoleInfo=group.Key, Child=group.ToList() }).ToList();
            //await Logger.AddPlatformLog($"获取{(type == 1 ? "用户" : "部门")}", LogType.DataAccess);
            return new AjaxResult<List<ResultCompleteInfoDto>>(resultData);
        }
        /// <summary>
        /// 获取加密模式
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<AjaxResult<List<ResultSecretModelDto>>> GetSecretMode() {

            var result = await _smService.GetEntitys(i => 1 == 1);
            //await Logger.AddPlatformLog("获取加密模式", LogType.DataAccess);
            return new AjaxResult<List<ResultSecretModelDto>>(_map.Map<List<ResultSecretModelDto>>(result));
        
        }

        /// <summary>
        /// 根据数据Id获取映射表字段
        /// </summary>
        /// <param name="dataid"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<AjaxResult<List<ResultMainFieldInfoDto>>> GetFiledInfo(int dataid)
        {
            string sql = $" select Id,GUID,AssociativeTable,IsDataService from datamap where id='{dataid}'  ";
            var mapdatainfo = await _dtService.GetEntity<ResultMapInfoDto>(sql);
            if (mapdatainfo == null) return new AjaxResult<List<ResultMainFieldInfoDto>>(false,"数据错误");
            sql = $" select CnFieldName fieldkey,EnFieldName filedvalue  from {(!mapdatainfo.IsDataService.HasValue? "data_sharing_affiliated" : "data_sharing_main")}.dic_{mapdatainfo.AssociativeTable}   ";
            var result = await _dtService.GetEntityList<ResultMainFieldInfoDto>(sql);
            //await Logger.AddPlatformLog($"获取数据:{dataid}的映射表字段", LogType.DataAccess);
            return new AjaxResult<List<ResultMainFieldInfoDto>>(result);
        }
        /// <summary>
        /// 数据加密
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<string>> DataEncryption(DataEncryptionInfoDto dto)
        {
            string sql = $" select Id,GUID,AssociativeTable from datamap where guid='{dto.GUID}'  ";
            var mapdatainfo = await _dtService.GetEntity<ResultMapInfoDto>(sql);
            if (mapdatainfo == null) return new  AjaxResult<string>(false);
            sql = $" select id from secret where mapid={mapdatainfo.Id} ";
            var secretid = await _dtService.QueryCount(sql);
            if (secretid != null) return new AjaxResult<string>(false, "该数据表已存在加密！");

            var resultsecretId = await await Task.Factory.StartNew(async () => {
              
              var secretid= await _secretService.IdAdd(new Models.secret { MapId = mapdatainfo.Id, SecretEncode = dto.Key, SecretMode = dto.SecoretMode, SecretStatus = 1, SecretType = dto.Type });
                if (dto.Type == 1)
                {
                    var resultsc = await _scService.Adds(dto.Obj.Select(i => new secret_child { Parent_id = secretid, SecretKey = i }).ToList());
                }
                return secretid;
            });
            //await Logger.AddPlatformLog("数据加密", LogType.DataOperation);
            return new AjaxResult<string>(resultsecretId>0);
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<string>> DataDecryption(DataDecryptionInfoDto dto)
        {
           var mapEntity=await _mapService.GetEntity(i=>i.GUID.Equals(dto.GUID));
            if(mapEntity == null) return new AjaxResult<string>(false);
            var secrtEntitys = await _secretService.GetEntitys(i => i.MapId == mapEntity.Id);
            if (secrtEntitys.Count > 0)
            {
                if (dto.Type == 1)
                {
                    await _secretService.Delete(i=>i.MapId==mapEntity.Id);
                }else if (dto.Type == 2)
                {
                    secrtEntitys.ForEach(async i => {
                        i.SecretStatus = 2;
                        i.StartTime = dto.StartTime;
                        i.EndTime=dto.EndTime;
                        await _secretService.Update(i);
                    });
                }
            }
            //await Logger.AddPlatformLog("解密", LogType.DataOperation);
            return new AjaxResult<string>();
        }

        /// <summary>
        /// 获取分表选项
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public Task<AjaxResult<List<ReturnDataChildInfoDto>>> GetDataChild()
        {


            return null;
        }

    }
}
