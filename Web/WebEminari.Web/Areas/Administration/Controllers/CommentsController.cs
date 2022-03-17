using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using WebEminari.Services.Data;
using WebEminari.Web.ViewModels;
using WebEminari.Web.ViewModels.Administration;
using WebEminari.Web.ViewModels.HelperModels;

namespace WebEminari.Web.Areas.Administration.Controllers
{
    public class CommentsController : AdministrationController
    {
        private readonly ICommentsService commentsService;

        public CommentsController(ICommentsService commentsService)
            => this.commentsService = commentsService;

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var allComments = await this.commentsService.GetAllCommentsAsync();
         

            return this.View(allComments);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            AdminCommentEditModel comment = await this.commentsService.GetCommentByIdWithDeletedAsync<AdminCommentEditModel>(id);
            return this.View(comment);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, AdminCommentEditModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.commentsService.EditCommentByAdminAsync(model);
            return this.RedirectToAction(nameof(this.Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            AdminCommentDeleteModel comment = await this.commentsService.GetCommentByIdWithDeletedAsync<AdminCommentDeleteModel>(id);
            return this.View(comment);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await this.commentsService.HardDeleteCommentByIdAsync(id);
            return this.RedirectToAction(nameof(this.Index));
        }
    }
}

