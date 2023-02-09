using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Threading.Tasks;

namespace ProgramsNetCore.PolicyRequirement
{
    public class MustRoleHandler : AuthorizationHandler<AdminRequirement> //官方抽象类 比IAuthorizationHandler拓展性更高
    {
        //public Task HandleAsync(AuthorizationHandlerContext context)
        //{
        //    context.Succeed(context.Requirements.FirstOrDefault());
        //  return Task.CompletedTask;
        //}
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminRequirement requirement)
        {

            return Task.CompletedTask;
        }
    }
}
