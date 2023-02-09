using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Models.Dto.DataMigrationDto
{
    public class GetContextUserInfoDto
    {
        public int Id { get; set; }
        public int UserName { get; set; }
        public int DepartmentId { get; set; }
        public int Department { get; set; }
    }
}
