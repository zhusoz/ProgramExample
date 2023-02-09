using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Models.Dto.DataSecurity
{
    public class ResultCompleteInfoDto
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string UserName { get; set; }
        public int? UserId { get; set; }

        public List<ResultCompleteInfoDto> Child { get; set; }
    }
}
