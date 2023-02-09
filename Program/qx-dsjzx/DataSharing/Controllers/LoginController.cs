using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProblemDisposal.Common.Basic;
using ProgramsNetCore.Common.JWT;
using ProgramsNetCore.IService;
using ProgramsNetCore.Models.Dto.LoginDto;
using ProgramsNetCore.Models.Dto.TokenUserDto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using ProgramsNetCore.Common;

namespace DataSharing.Controllers
{
    [Route("")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IOrgService _orgService;
        private readonly IDataTableService _dtService;
        private readonly IMapper _map;

        public LoginController(IOrgService orgService, IDataTableService dtService, IMapper map)
        {
            _orgService=orgService;
            _dtService = dtService;
            _map = map;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<LoginTokenInfoDto>> Login(LoginDto dto)
        {
            //string loginsql = $"select cu.id,cu.account ,cu.real_name ,cu.description departmentid,co.Title department,IFNULL((select GROUP_CONCAT(roleid,',')   from (  select *  from  `cloud_user_role` where UserId=cu.id and type=1 UNION  select *  from  `cloud_user_role` where UserId=cu.`description` and type=2  ) u_r )  ,'') role  from cloud_user cu left join cloud_org co on co.id=cu.description where account='{dto.Account}' and Password='{dto.Password}' ";

            string loginsql = $"select cu.id,cu.account ,cu.real_name ,cu.description departmentid,co.Title department from cloud_user cu left join cloud_org co on co.id=cu.description where account='{dto.Account}' and Password='{dto.Password}' ";
            var resultLoginInfo = await _dtService.GetEntity<ResultLoginInfoDto>(loginsql);
            if (resultLoginInfo == null) return new AjaxResult<LoginTokenInfoDto>(false, "账户或密码错误");
            loginsql = $"select GROUP_CONCAT(roleid,',')   from (  select *  from  `cloud_user_role` where UserId={resultLoginInfo.Id} and type=1 UNION  select *  from  `cloud_user_role` where UserId={resultLoginInfo.DepartmentId} and type=2  ) u_r ";
            var roleinfo = await _dtService.QueryCount(loginsql);
            resultLoginInfo.Role = roleinfo == null ? "" : roleinfo.ToString();
          
            var strtoken = JwtHelper.IssueJwt(new TokenModel { UId=resultLoginInfo.Id, UName=resultLoginInfo.Real_Name, Role=resultLoginInfo.Role });
            var result = _map.Map<LoginTokenInfoDto>(resultLoginInfo);
            result.Token = strtoken;

            //await Logger.AddPlatformLog("用户登录", LogType.Login);

            return new AjaxResult<LoginTokenInfoDto>(result);
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<string>> ChangePwd(ChangePwdDto dto)
        {
            var loginsql = $"select Id from cloud_user  where account='{dto.Account}' and Password='{dto.OldPassword}' ";
            var exists = await _dtService.QueryCount(loginsql);
            if (exists == null) return new AjaxResult<string>(false, "账号或密码错误");
            string updatesql = $" update cloud_user set password='{dto.NowPassword}' where account='{dto.Account}'  ";
            var result = await _dtService.NoQueryExcuteSql(updatesql);

            //await Logger.AddPlatformLog("修改密码", LogType.DataOperation);

            return new AjaxResult<string>(result>0);
        }

        /// <summary>
        /// 获取组织架构
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<AjaxResult<List<ResultOrgTreeDto>>> GetOrgStructure(int? pid = null)
        {
            var result = await await Task.Factory.StartNew(async () =>
            {
                string sql = "select id,path,creater,title,parent_Id as ParentId from cloud_org ";
                var list = await _dtService.GetEntityList<ResultOrgTreeDto>(sql);
                list = await GetTreeOrg(list, new List<ResultOrgTreeDto>(), pid);
                return list;
            });

            //await Logger.AddPlatformLog("获取组织架构", LogType.DataAccess);

            return new AjaxResult<List<ResultOrgTreeDto>>(result);
        }

        private async Task<List<ResultOrgTreeDto>> GetTreeOrg(List<ResultOrgTreeDto> treeNodes, List<ResultOrgTreeDto> resps, int? pID)
        {
            resps = new List<ResultOrgTreeDto>();
            List<ResultOrgTreeDto> tempList = treeNodes.Where(c => c.ParentId == pID).ToList();

            for (int i = 0; i < tempList.Count; i++)
            {
                ResultOrgTreeDto node = new ResultOrgTreeDto();
                node.Id = tempList[i].Id;
                node.ParentId = tempList[i].ParentId;
                node.AreaId = tempList[i].AreaId;
                node.Title = tempList[i].Title;
                node.Path = tempList[i].Path;
                node.Type = tempList[i].Type;
                node.Children = await GetTreeOrg(treeNodes, resps, node.Id);
                resps.Add(node);
            }

            return resps;
        }


        /// <summary>
        /// 组织架构-增
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<bool>> OrgAdd(OrgAddAndEditDto dto)
        {
            var ret = await _orgService.Add(new Models.cloud_org
            {
                Creater=dto.Creater,
                Gmt_create=dto.GmtCreate.HasValue ? dto.GmtCreate.Value : System.DateTime.Now,
                Parent_id=dto.ParentId,
                Path=dto.Path,
                Title=dto.Title
            });

            //await Logger.AddPlatformLog("组织架构-增", LogType.DataOperation);
            
            return new AjaxResult<bool>(ret);
        }


        /// <summary>
        /// 组织架构-删
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<bool>> OrgDel([FromBody] int id)
        {
            bool ret1 = await _orgService.Delete(e => e.Parent_id == id);
            bool ret2 = await _orgService.Delete(e => e.Id==id);

            //await Logger.AddPlatformLog("组织架构-删", LogType.DataOperation);
            
            return new AjaxResult<bool>(ret1 && ret2);
        }

        /// <summary>
        /// 组织架构-改
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<bool>> OrgEdit(OrgAddAndEditDto dto)
        {
            if (!dto.Id.HasValue)
            {
                return new AjaxResult<bool>(false, "Param `Id` can't be null");
            }
            var org = await _orgService.GetEntity(e => e.Id==dto.Id);
            if (org is null)
            {
                return new AjaxResult<bool>(false, "Can't find the org");
            }

            var ret = await _orgService.Update(new Models.cloud_org
            {
                Creater = dto.Creater,
                Gmt_create=dto.GmtCreate.HasValue ? dto.GmtCreate.Value : org.Gmt_create,
                Id=org.Id,
                Parent_id = dto.ParentId,
                Path=dto.Path,
                Title=dto.Title
            });

            //await Logger.AddPlatformLog("组织架构-改", LogType.DataOperation);

            return new AjaxResult<bool>(ret);
        }


    }
}
