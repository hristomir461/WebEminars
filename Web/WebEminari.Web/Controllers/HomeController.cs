namespace WebEminari.Web.Controllers
{
    using System.Diagnostics;

    using WebEminari.Web.ViewModels;

    using Microsoft.AspNetCore.Mvc;
    using WebEminari.Web.ViewModels.Home;
    using WebEminari.Services.Data;
    using WebEminari.Web.ViewModels.WebEminars;
    using WebEminari.Data.Models;
    using WebEminari.Data.Common.Repositories;

    public class HomeController : BaseController
    {
        private readonly IWebEminarsService webEminarService;
        private readonly IDeletableEntityRepository<WebEminar> webEminarRepository;

        public HomeController(IWebEminarsService webEminarService, IDeletableEntityRepository<WebEminar> webEminarRepository)
        {
            this.webEminarService = webEminarService;
            this.webEminarRepository = webEminarRepository;
        }

        public IActionResult Index()
        {

            var viewModel = new IndexViewModel()
            {
                RandomWebEminars = this.webEminarService.GetRandom<IndexPageWebEminarViewModel>(3),
                Webinars = this.webEminarService.GetAllWebinars<WebEminarViewModel>(),
                KacheniWebinars = this.webEminarService.GetAllWebinars<WebEminarViewModel>(),
            };

            return this.View(viewModel);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
