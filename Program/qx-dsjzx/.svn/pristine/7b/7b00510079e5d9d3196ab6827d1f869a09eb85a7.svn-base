using Microsoft.AspNetCore.Authorization;

namespace ProgramsNetCore.PolicyRequirement
{
    public class PermissionRequirement:IAuthorizationRequirement
    {
        //权限名称
        public string Permission { get; }

        public PermissionRequirement(string permission)
        {
            Permission = permission;
        }

    }
}
