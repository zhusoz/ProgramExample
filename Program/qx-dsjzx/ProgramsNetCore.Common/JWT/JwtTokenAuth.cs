using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using MySqlX.XDevAPI;

namespace ProgramsNetCore.Common.JWT
{
    public class JwtTokenAuth
    {
        // 中间件一定要有一个next，将管道可以正常的走下去
        private readonly RequestDelegate _next;

        public JwtTokenAuth(RequestDelegate next)
        {
            _next = next;
        }
public Task Invoke(HttpContext httpContext)
{

            var headers = httpContext.Request.Headers;
       
            //检测是否包含'Authorization'请求头，如果不包含返回context进行下一个中间件，用于访问不需要认证的API
            if (!headers.ContainsKey("Authorization"))
            {
                return _next(httpContext);
            }

            var tokenHeader = httpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            try
            {
                if (tokenHeader.Length >= 128)
                {
                    //Console.WriteLine($"{DateTime.Now} token :{tokenHeader}");
                    TokenModel tm = JwtHelper.SerializeJwt(tokenHeader);

                    //授权
                    var claimList = new List<Claim>();
                    if (tm.Role.Contains(@"\"))
                    {
                        var list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(tm.Role);
                        //var claim = new Claim(ClaimTypes.Role, tm.Role);
                        claimList.AddRange(list.Where(i => !string.IsNullOrEmpty(i)).Select(i => new Claim(ClaimTypes.Role, i)));
                    }else
                    {
                        claimList.AddRange(tm.Role.Split(',').Where(i => !string.IsNullOrEmpty(i)).Select(i=>new Claim(ClaimTypes.Role,i)));
                    }
                    //Test-By-ZSZ
                    claimList.Add(new Claim("Permission",tm.Permissions));
                    claimList.Add(new Claim(JwtRegisteredClaimNames.NameId, tm.UId.ToString()));
                    claimList.Add(new Claim(ClaimTypes.NameIdentifier, tm.UId.ToString()));

                    //claimList.Add(claim);
                    var identity = new ClaimsIdentity(claimList);
                    var principal = new ClaimsPrincipal(identity);
                    httpContext.User = principal;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine($"{DateTime.Now} middleware wrong:{e.Message}");
            }

            return _next(httpContext);
        }

    }
    // 这里定义一个中间件Helper，主要作用就是给当前模块的中间件取一个别名
    public static class MiddlewareHelpers
    {
        public static IApplicationBuilder UseJwtTokenAuth(this IApplicationBuilder app)
        {
            return app.UseMiddleware<JwtTokenAuth>();
        }
    }
}

