using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

using Microsoft.AspNetCore.SignalR;

namespace WebEminari.Web.Hubs
{
    public class ChatHub : Hub
    {
        [HttpPost]
        public Task JoinRoom(string roomId) => this.Groups.AddToGroupAsync(this.GetConnectionId(), roomId);

        [HttpPost]
        public Task LeaveRoom(string roomId) => this.Groups.RemoveFromGroupAsync(this.GetConnectionId(), roomId);

        private string GetConnectionId() => this.Context.ConnectionId;
    }
}
