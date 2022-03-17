using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using WebEminari.Data.Models;
using WebEminari.Services.Data;

namespace WebEminari.Web.ViewComponents
{
    public class Room : ViewComponent
    {
        private readonly IChatService chatService;

        public Room(IChatService chatService)
            => this.chatService = chatService;

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Chat> chats = await this.chatService.GetAllChatsAsync();
            return this.View(chats);
        }
    }
}