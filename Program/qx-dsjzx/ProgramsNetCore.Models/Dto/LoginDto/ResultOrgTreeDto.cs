using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Models.Dto.TokenUserDto
{
    public class ResultOrgTreeDto
    {
        public string Path { get; set; }
        public int AreaId { get; set; }
        public List<ResultOrgTreeDto> Children { get; set; }
        public int Creater { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public int? ParentId { get; set; }
    }
}
