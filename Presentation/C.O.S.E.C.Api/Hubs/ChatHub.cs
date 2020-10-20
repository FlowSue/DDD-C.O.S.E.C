using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace C.O.S.E.C.Api.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("SendMessage", user, message).ConfigureAwait(false);
        }
    }
}
