using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WebEminari.Data.Models;

namespace WebEminari.Services.Data
{
    public interface IChatService
    {
        Chat GetChat(int id);

        IEnumerable<Chat> GetChats(string userId);

        Task<List<Chat>> GetAllChatsAsync();

        Task<int> CreateRoomAsync(string name, string userId);

        Task JoinRoomAsync(int chatId, string userId);

        Task<Message> CreateMessageAsync(int chatId, string message, string userName);
    }
}
