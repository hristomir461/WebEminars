using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using WebEminari.Services.Data;
using WebEminari.Web.ViewModels.Votes;

namespace WebEminari.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VotesController : BaseController
    {
        private readonly IVotesService votesService;

        public VotesController(IVotesService votesService)
        {
            this.votesService = votesService;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<PostVoteResponseModel>> Post(PostVoteInputModel input)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            await this.votesService.SetVoteAsync(input.WebEminarId, userId, input.Value);

            var averageVotes = this.votesService.GetAverageVotes(input.WebEminarId);

            return new PostVoteResponseModel() { AverageVote = averageVotes };
        }
    }
}
