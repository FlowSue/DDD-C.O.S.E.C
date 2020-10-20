using C.O.S.E.C.Api.Hubs;
using C.O.S.E.C.Domain.Enums.Auth;
using C.O.S.E.C.Infrastructure.Auth.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace C.O.S.E.C.Api.Controllers
{
    /// <summary>
    /// IM
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthPolicyEnum.RequireRoleOfAdmin)]
    public class SignalRController : ControllerBase
    {
        private readonly IHubContext<ChatHub> hubContext;

        public SignalRController(IServiceProvider service)
        {
            this.hubContext = service.GetService<IHubContext<ChatHub>>(); ;
        }
        [HttpGet]
        public void SendMessage(string message) => hubContext.Clients.All.SendAsync("SendMessage", $"{message}");

        [HttpPost]
        public void Group(string groupname, string message) => hubContext.Clients.Group(groupname).SendAsync("SendMessage", $"{message}");

        [HttpPost]
        public void Hello(string connectid) => hubContext.Clients.Client(connectid).SendAsync("SendMessage", "");

        [HttpPost]
        public void AddToGroup(string connectid, string groupname) => hubContext.Groups.AddToGroupAsync(connectid, groupname);

        [HttpPost]
        public void ExitToGroup(string connectid, string groupname) => hubContext.Groups.RemoveFromGroupAsync(connectid, groupname);
    }
}