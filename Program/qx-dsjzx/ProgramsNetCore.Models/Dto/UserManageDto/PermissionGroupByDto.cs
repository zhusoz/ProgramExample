using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Models.Dto.UserManageDto
{
    public class PermissionGroupByDto
    {

        public string ModuleUrl { get; set; }

        public string ModuleName { get; set; }

        public int ParentId { get; set; }

        public List<PermissionDataDto> PermissionlList { get; set; }
    }
}
