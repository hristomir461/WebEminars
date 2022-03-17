using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using WebEminari.Services.Data;
using WebEminari.Web.ViewModels.Comments;

namespace WebEminari.Web.ViewComponents
{
    public class AllComments : ViewComponent
    {
        private readonly IViewComponentsService viewComponentsService;

        public AllComments(IViewComponentsService viewComponentsService)
            => this.viewComponentsService = viewComponentsService;

        public async Task<IViewComponentResult> InvokeAsync(int webEminarId)
        {
            IEnumerable<CommentViewModel> commentViewModels = await this.viewComponentsService.GetCommentsAsync(webEminarId);
            return this.View(commentViewModels);
        }
    }
}
