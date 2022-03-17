using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WebEminari.Web.ViewModels;
using WebEminari.Web.ViewModels.Administration;
using WebEminari.Web.ViewModels.Comments;

namespace WebEminari.Services.Data
{
    public interface ICommentsService
    {
        int GetAllCommentsCount();

        bool Contains(int id);

        Task<T> GetCommentByIdWithDeletedAsync<T>(int id);

        Task<IEnumerable<AdminCommentViewModel>> GetAllCommentsAsync();

        Task<IEnumerable<CommentByMeModel>> GetCommentsMadeByMeAsync(string userId);

        Task<IEnumerable<CommentViewModel>> GetCommentsMadeInMyWebEminarAsync(string userId);

        Task CreateCommentAsync(CommentInputModel commentViewModel, int? parentId);

        Task EditCommentAsync(CommentEditModel commentEditModel);

        Task EditCommentByAdminAsync(AdminCommentEditModel commentEditModel);

        Task SoftDeleteCommentByIdAsync(int commentId);

        Task HardDeleteCommentByIdAsync(int commentId);
    }
}
