using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Models.Dto.DataMigration
{
    /// <summary>
    /// 加密信息
    /// </summary>
    public class ResultSecretDataInfoDto
    {
        public int Id { get; set; }
        public string GUID { get; set; }
        public string AssociativeTable { get; set; }
        public string TableName { get; set; }
        public int SecretId { get; set; }
        public int SecretStatus { get; set; }
        public string SecretEncode { get; set; }
        public int? SecretType { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int SecretMode { get; set; }
        public bool? IsDataService { get; set; }
        public bool? IsMapping { get; set; }
    }
}
