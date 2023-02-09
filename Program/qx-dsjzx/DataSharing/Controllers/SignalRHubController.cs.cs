using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using ProblemDisposal.Common.Basic;
using ProgramsNetCore.Common;
using ProgramsNetCore.Common.Hubs;
using System.Threading.Tasks;

namespace DataSharing.Controllers
{
    /// <summary>
    /// SignalR网络通信
    /// </summary>
    [Route("")]
    [ApiController]
    public class SignalRHubController : ControllerBase
    {
        private readonly IHubContext<PushHub> _hubContext;

        public SignalRHubController(IHubContext<PushHub> hubContext)
        {
            _hubContext = hubContext;
        }

        /// <summary>
        /// 发送信息，接收‘ReceiveMessage’
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<AjaxResult<string>> SendMessage(string user,string msg)
        {
           await _hubContext.Clients.User(user).SendAsync("ReceiveMessage",user, msg);
            System.Console.WriteLine($"{user}  {msg}");

            //await Logger.AddPlatformLog("发送消息", LogType.DataOperation);
            
            return new AjaxResult<string>();
        }

        
    }
}
