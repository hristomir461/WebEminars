using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WebEminari.Web.ViewModels.Administration;

namespace WebEminari.Services.Data
{
    public class InformationService : IInformationService
    {
        private readonly ICategoriesService categoriesService;
        private readonly IWebEminarsService webinarsService;
        private readonly IReportsService reportsService;
        private readonly ILikesService likesService;
        private readonly IVotesService votesService;
        private readonly ICommentsService commentsService;
        private readonly IUsersService usersService;

        public InformationService(
            ICategoriesService categoriesService,
            IWebEminarsService webinarsService,
            IReportsService reportsService,
            IVotesService votesService,
            ILikesService likesService,
            ICommentsService commentsService,
            IUsersService usersService)
        {
            this.webinarsService = webinarsService;
            this.categoriesService = categoriesService;
            this.reportsService = reportsService;
            this.likesService = likesService;
            this.votesService = votesService;
            this.commentsService = commentsService;
            this.usersService = usersService;
        }
        public async Task<AdministrationIndexViewModel> GenerateAdministrationIndexViewModel()
            => new AdministrationIndexViewModel
            {
                CategoriesCount = this.categoriesService.GetCount(),
                UsersCount = this.usersService.GetCount(),
                WebinarsCount = this.webinarsService.GetCount(),
                ReportsCount = this.reportsService.GetCount(),
                CommentsCount = this.commentsService.GetAllCommentsCount(),
                LikesCount = this.likesService.GetCount(),
                VotesCount = this.votesService.GetCount(),
            };
  }
}
