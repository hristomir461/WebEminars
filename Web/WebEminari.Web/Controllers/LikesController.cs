using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using WebEminari.Data.Models;
using WebEminari.Services.Data;
using WebEminari.Web.ViewModels.Likes;
using WebEminari.Web.ViewModels.Votes;

namespace WebEminari.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikesController : BaseController
    {
        private readonly ILikesService likesService;
        private readonly UserManager<ApplicationUser> userManager;

        public LikesController(
            ILikesService likesService,
            UserManager<ApplicationUser> userManager)
        {
            this.likesService = likesService;
            this.userManager = userManager;
        }

        // POST /api/votes
        // Request body: {"postId":1}
        // Response body: {"likesCount":3}
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<LikeResponseModel>> ToggleLike(LikeInputModel model)
        {
            var userId = this.userManager.GetUserId(this.User);
            await this.likesService.ToggleAsync(model.WebEminarId, userId);

            var likesCount = this.likesService.GetCountByLessonId(model.WebEminarId);

            return new LikeResponseModel { LikesCount = likesCount };
        }
    }
}
