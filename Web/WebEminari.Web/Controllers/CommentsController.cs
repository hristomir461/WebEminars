using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

using WebEminari.Data;
using WebEminari.Data.Models;
using WebEminari.Services.Data;
using WebEminari.Services.Messaging;
using WebEminari.Web.ViewModels.Comments;
using WebEminari.Web.ViewModels.WebEminars;

namespace WebEminari.Web.Controllers
{
    public class CommentsController : BaseController
    {

        private readonly ICommentsService commentsService;

        public CommentsController(ICommentsService commentsService)
            => this.commentsService = commentsService;

        [HttpGet]
        public IActionResult Create() => this.RedirectToAction("All", "WebEminars");

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CommentInputModel commentViewModel)
        {
            var parentId =
               commentViewModel.ParentId == 0 ?
                   (int?)null :
                   commentViewModel.ParentId;

            commentViewModel.ApplicationUserId = this.GetUserId();
            var id = new { id = commentViewModel.WebEminarId };

            if (!this.ModelState.IsValid)
            {
                // var changedModel = new LessonViewModel { LessonId = commentViewModel.LessonId };
                return this.RedirectToAction("Details", "WebEminars", id);
            }

            await this.commentsService.CreateCommentAsync(commentViewModel, parentId);
            return this.RedirectToAction("Details", "WebEminars", id);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            CommentEditModel comment = await this.commentsService.GetCommentByIdWithDeletedAsync<CommentEditModel>(id);
            return this.View(comment);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(CommentEditModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            if (model.ApplicationUserId != this.GetUserId())
            {
                return this.View(nameof(this.Unauthorized));
            }

            await this.commentsService.EditCommentAsync(model);
            return this.RedirectToAction(nameof(this.MyComments));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var commentModel = await this.commentsService.GetCommentByIdWithDeletedAsync<CommentDeleteModel>(id);
            return this.View(commentModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Delete(CommentDeleteModel model)
        {
            if (model.ApplicationUserId != this.GetUserId())
            {
                return this.View("Unauthorized");
            }

            await this.commentsService.SoftDeleteCommentByIdAsync(model.Id);
            return this.RedirectToAction(nameof(this.MyComments));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> MyComments()
        {
            IEnumerable<CommentByMeModel> commentByMe = await this.commentsService.GetCommentsMadeByMeAsync(this.GetUserId());
            return this.View(commentByMe);
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> WebEminarsComments()
        {
            IEnumerable<CommentViewModel> comments = await this.commentsService.GetCommentsMadeInMyWebEminarAsync(this.GetUserId());
            return this.View(comments);
        }
    }
}
