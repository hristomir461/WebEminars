using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WebEminari.Data.Models;

namespace WebEminari.Services.Data
{
    public interface ILikesService
    {
        int GetCount();
        Task ToggleAsync(int webEminarId, string userId);

        Task AddAsync(int webEminarId, string userId);

        Task RemoveAsync(Like like);

        int GetCountByLessonId(int webEminarId);
    }
}
