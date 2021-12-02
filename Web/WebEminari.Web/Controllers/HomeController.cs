namespace WebEminari.Web.Controllers
{
    using System.Diagnostics;

    using WebEminari.Web.ViewModels;

    using Microsoft.AspNetCore.Mvc;
    using WebEminari.Web.ViewModels.Home;
    using WebEminari.Services.Data;

    public class HomeController : BaseController
    {
        private readonly IWebEminarsService webEminarService;

        public HomeController(IWebEminarsService webEminarService)
        {
            this.webEminarService = webEminarService;
        }

        public IActionResult Index()
        {

            var viewModel = new IndexViewModel()
            {
                RandomWebEminars = this.webEminarService.GetRandom<IndexPageWebEminarViewModel>(10),
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
