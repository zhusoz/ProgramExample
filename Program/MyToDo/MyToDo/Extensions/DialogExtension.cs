using MyToDo.Common;
using MyToDo.Events;
using Prism.Events;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Extensions
{
    public static class DialogExtension
    {
        /// <summary>
        /// 询问窗口
        /// </summary>
        /// <param name="dialogHost">指定的DialogHost会话主机</param>
        /// <param name="title">标题</param>
        /// <param name="content">内容</param>
        /// <param name="dialogHostName">会话主机名称(唯一)</param>
        /// <returns></returns>
        public static async Task<IDialogResult> Question(this IDialogHostService dialogHost, string title, string content, string dialogHostName = "Root")
        {
            DialogParameters parameters = new DialogParameters();
            parameters.Add("Title", title);
            parameters.Add("Content", content);
            parameters.Add("dialogHostName", dialogHostName);
            IDialogResult result = await dialogHost.ShowDialog("MsgView", parameters, dialogHostName);
            return result;
        }

        public static void UpdateLoading(this IEventAggregator aggregator, UploadModel model)
        {
            aggregator.GetEvent<UploadingEvent>().Publish(model);
        }

        public static void Register(this IEventAggregator aggregator, Action<UploadModel> action)
        {
            aggregator.GetEvent<UploadingEvent>().Subscribe(action);
        }

        /// <summary>
        /// 注册提示消息
        /// </summary>
        /// <param name="aggregator"></param>
        /// <param name="action">推送消息时触发的方法</param>
        public static void RegisterMessage(this IEventAggregator aggregator, Action<MessageModel> action, string path = "Main")
        {
            aggregator.GetEvent<MessageEvent>().Subscribe(action, ThreadOption.PublisherThread, true, e =>
            {
                return e.Path.Equals(path);
            });
        }

        /// <summary>
        /// 推送消息
        /// </summary>
        /// <param name="aggregator"></param>
        /// <param name="message">消息</param>
        /// <param name="path">消息推送路径</param>
        public static void SendMessage(this IEventAggregator aggregator, string message, string path = "Main")
        {
            aggregator.GetEvent<MessageEvent>().Publish(new MessageModel { Message=message, Path=path });
        }

    }
}
