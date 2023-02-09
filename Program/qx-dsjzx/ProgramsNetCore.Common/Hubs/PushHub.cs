using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using ProgramsNetCore.Common.JWT;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProgramsNetCore.Common.Hubs
{
    public class PushHub:Hub
    {
        private readonly IHttpContextAccessor _httpContext;

        public PushHub(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public override Task OnConnectedAsync()
        {
            
            return base.OnConnectedAsync();
        }
        public async Task SendMessage(string user,string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
