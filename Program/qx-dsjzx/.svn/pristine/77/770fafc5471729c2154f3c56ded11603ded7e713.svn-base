using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProgramsNetCore.PolicyRequirement
{
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>//官方抽象类 比IAuthorizationHandler拓展性更高
    {
        public IAuthenticationSchemeProvider Schemes { get; set; }
        //private readonly IDistributedCache _cacheService;

        /// <summary>
        /// 构造函数注入
        /// </summary>
        public PermissionHandler(IAuthenticationSchemeProvider schemes)
        {
            Schemes = schemes;
        }


        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            
            if (!context.User.HasClaim(c => c.Type == "Permission"))
            {
                return Task.CompletedTask;
            }

            string permissions = context.User.FindFirst(c => c.Type == "Permission").Value;
            
            //如果该用户权限列表包含该权限则授权
            if (permissions.Split(',').Contains(requirement.Permission))
            {
                context.Succeed(requirement);
            }
            
            return Task.CompletedTask;
        }
    }
}
