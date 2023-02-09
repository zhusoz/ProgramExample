using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Models.Dto.DataCollectDto
{
    public class GetClassifyManageQueryDto : PageDto
    {
        /// <summary>
        /// 是否已标记
        /// </summary>
        public bool isAlreadyTag { get; set; }
        
    }
}
