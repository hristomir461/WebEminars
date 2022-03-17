using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using WebEminari.Data.Common.Repositories;
using WebEminari.Data.Models;
using WebEminari.Services.Mapping;
using WebEminari.Web.ViewModels.Comments;

namespace WebEminari.Services.Data
{
    public class ViewComponentsService : IViewComponentsService
    {
        private readonly IRepository<Comment> commentRepository;
        public ViewComponentsService(
            IRepository<Comment> commentRepository)
        {
            this.commentRepository = commentRepository;
        }
        public async Task<IEnumerable<CommentViewModel>> GetCommentsAsync(int webEminarId)
            => await this.commentRepository
                .All()
                .Where(x => x.WebEminar.Id == webEminarId && !x.IsDeleted)
                .Include(x => x.ApplicationUser)
                .OrderByDescending(x => x.CreatedOn)
                .To<CommentViewModel>()
                .ToArrayAsync();
    }
}
