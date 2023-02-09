using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Models.Dto.DataCollectDto
{
    /// <summary>
    /// EditShow dto
    /// </summary>
    public class EditShowResultDto
    {
        public int Id { get; set; }
        public string DataMapEnName { get; set; }
        public string Name { get; set; }
        public string Guid { get; set; }
        public string Describe { get; set; }
        public string InfoSummary { get; set; }
        public string ApplicationNum { get; set; }
        public string ApplicationSystemName { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string LinkPerson { get; set; }
        public string LinkPhone { get; set; }
        public string Source { get; set; }
        public int AttributionId { get; set; }
        public int FrequencyId { get; set; }
        public int LayeredId { get; set; }
        public int DataTypeId { get; set; }
        public string AssociativeTable { get; set; }
        public int RackStatus { get; set; }
        public int Status { get; set; }
        public List<ResultDataItemDicDto> DataItems { get; set; }
    }
}
