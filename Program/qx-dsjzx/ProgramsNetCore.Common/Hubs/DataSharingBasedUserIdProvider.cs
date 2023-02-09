using Microsoft.AspNetCore.SignalR;
using System.Linq;
using System.Security.Claims;

namespace ProgramsNetCore.Common.Hubs
{
    public class DataSharingBasedUserIdProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection)
        {

            var str = connection.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return connection.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
