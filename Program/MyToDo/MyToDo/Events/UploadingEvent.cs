using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Events
{
    public class UploadModel
    {
        /// <summary>
        /// 是否打开
        /// </summary>
        public bool IsOpen { get; set; }

    }

    public class UploadingEvent : PubSubEvent<UploadModel>
    {

    }
}
