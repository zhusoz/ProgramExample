using Microsoft.AspNetCore.Mvc.Filters;
using ProgramsNetCore.IService;
using System;
using System.Threading.Tasks;

namespace DataSharing.Filter
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class PermissionFilterAttribute : Attribute, IAsyncActionFilter,IAsyncAuthorizationFilter,IAuthorizationFilter
    {
        /// <summary>
        /// 权限名称
        /// </summary>
        public string PermissionName { get; set; }

        //Autofac 属性注入
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IPermissionService _permissionService;
        private readonly IUserRoleService _userRoleService;
        private readonly IRolePermissionService _rolePermissionService;

        public PermissionFilterAttribute(string permissionName, IUserService userService, IRoleService roleService, IPermissionService permissionService, IUserRoleService userRoleService, IRolePermissionService rolePermissionService)
        {
            PermissionName = permissionName;
            _userService = userService;
            _roleService = roleService;
            _permissionService = permissionService;
            _userRoleService = userRoleService;
            _rolePermissionService = rolePermissionService;

        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //1.获取当前登录用户
            var userId = 4702;//假定登录用户为4702

            //2.获取当前登录用户的角色权限
            var rolePermission = "";
            _userRoleService.GetEntitys(e => e.UserId==userId);

            //3.判断是否拥有该权限


            throw new NotImplementedException();
        }

        public Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            throw new NotImplementedException();
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            throw new NotImplementedException();
        }
    }
}
