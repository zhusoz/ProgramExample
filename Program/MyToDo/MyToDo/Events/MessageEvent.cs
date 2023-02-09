using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Events
{
    public class MessageModel
    {
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 消息路径
        /// </summary>
        public string Path { get; set; }
    }

    public class MessageEvent : PubSubEvent<MessageModel>
    {

    }
}
