using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WebEminari.Web.ViewModels.Comments;

namespace WebEminari.Services.Data
{
    public interface IViewComponentsService
    {
        Task<IEnumerable<CommentViewModel>> GetCommentsAsync(int webEminarId);
    }
}
