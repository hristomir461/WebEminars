namespace WebEminari.Web.Controllers
{
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
    using WebEminari.Data;
    using WebEminari.Data.Models;
    using WebEminari.Services.Data;
    using WebEminari.Services.Messaging;
    using WebEminari.Web.ViewModels.WebEminars;

    public class WebEminarsController : BaseController
    {
        private const int ItemsPerPage = 10;
        private readonly IEmailGridSender emailSender;
        private readonly ICategoriesService categoriesService;

        public IWebHostEnvironment WebHostEnviroment { get; }

        private readonly UserManager<ApplicationUser> userManager;

        private readonly IWebEminarsService webEminarService;

        private readonly ApplicationDbContext context;
         private readonly IInformationService informationService;

        public WebEminarsController(IInformationService informationService, IEmailGridSender emailSender, ICategoriesService categoriesService, IWebEminarsService webEminarService, ApplicationDbContext context, UserManager<ApplicationUser> userManager, IWebHostEnvironment webHostEnviroment)
        {
            this.userManager = userManager;
            this.context = context;
            this.WebHostEnviroment = webHostEnviroment;
            this.webEminarService = webEminarService;
            this.emailSender = emailSender;
            this.categoriesService = categoriesService;
            this.informationService = informationService;
        }


        [Authorize]
        public async Task<IActionResult> Index()
        {

            var model = await this.informationService.GenerateAdministrationIndexViewModel();

            return this.View(model);
        }

        [Authorize]
        public IActionResult IndexWebEminars()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var items = this.context.WebEminars.Where(webEminar => webEminar.AddedByUserId == userId).Where(webEminars => webEminars.ImageName != null).ToList();
            return this.View(items);
        }

        [Authorize]
        public IActionResult IndexWebEminarsWithVideos()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var items = this.context.WebEminars.Where(webEminar => webEminar.AddedByUserId == userId).Where(webEminars => webEminars.ImageName == null).ToList();
            return this.View(items);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> LikedByMe(string stateFilter, int categoryId, string searchString, int id = 1)
        {
            var userId = this.GetUserId();

            var viewModel = new WebEminarsListViewModel()
            {
                PageNumber = id,
                WebEminars = this.webEminarService.GetLikedByUserId<WebEminarsInListViewModel>(userId, stateFilter, id, ItemsPerPage, searchString, categoryId),
                WebEminarsCount = this.webEminarService.GetCount(),
                ItemsPerPage = ItemsPerPage,
                SearchText = searchString,
                StateFilter = stateFilter,
                CategoriesItems = this.categoriesService.GetAllAsKeyValuePairs(),
            };

            return this.View(viewModel);
        }

        public IActionResult All(string stateFilter, int categoryId, string searchString, int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            var viewModel = new WebEminarsListViewModel()
            {
                PageNumber = id,
                WebEminars = this.webEminarService.GetAll<WebEminarsInListViewModel>(stateFilter, id, ItemsPerPage, searchString, categoryId),
                Vebinari = this.webEminarService.GetAllWebinars<WebEminarViewModel>(),
                WebEminarsCount = this.webEminarService.GetCount(),
                ItemsPerPage = ItemsPerPage,
                SearchText = searchString,
                StateFilter = stateFilter,
                CategoriesItems = this.categoriesService.GetAllAsKeyValuePairs(),
            };

            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult Create()
        {
            var viewModel = new WebEminarViewModel();

            viewModel.CategoriesItems = this.categoriesService.GetAllAsKeyValuePairs();

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(WebEminarViewModel input)
        {
            if (!this.ModelState.IsValid || input.Image == null)
            {
                input.CategoriesItems = this.categoriesService.GetAllAsKeyValuePairs();

                return this.View(input);
            }

            string stringFile = this.UploadFile(input);
            var user = await this.userManager.GetUserAsync(this.User);


            await this.webEminarService.CreateAsync(input, user.Id, stringFile);

            return this.Redirect("IndexWebEminars");
        }


        [Authorize]
        public IActionResult CreateWithVideo()
        {
            var viewModel = new WebEminarWithVideoViewModel();

            viewModel.CategoriesItems = this.categoriesService.GetAllAsKeyValuePairs();

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateWithVideo(WebEminarWithVideoViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.CategoriesItems = this.categoriesService.GetAllAsKeyValuePairs();

                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            await this.webEminarService.CreateWithVideoAsync(input, user.Id);

            return this.Redirect("IndexWebEminarsWithVideos");
        }

        public IActionResult Details(int id)
        { 
            var webEminar = this.webEminarService.GetById<WebEminarViewModel>(id);
   
            return this.View(webEminar);
        }

        [Authorize]
        public IActionResult BookedPeople(int id)
        {
            var webEminar = this.webEminarService.GetById<WebEminarViewModel>(id);
            var userBookings = context.UserBookings.Where(x => x.WebEminarId == webEminar.Id);
            return this.View(userBookings);
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var items = this.context.WebEminars;
            if(items.Any(webEminar => webEminar.AddedByUserId == userId && webEminar.Id == id))
            {
                var inputModel = this.webEminarService.GetById<WebEminarViewModel>(id);

                inputModel.CategoriesItems = this.categoriesService.GetAllAsKeyValuePairs();

                return this.View(inputModel);
            }
            else
            {
                throw new ApplicationException();
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, WebEminarViewModel input)
        {
            //  this.ModelState["Image"].Errors.Clear();
            this.ModelState["Image"].ValidationState = ModelValidationState.Valid;
            if (!this.ModelState.IsValid)
            {
                input.CategoriesItems = this.categoriesService.GetAllAsKeyValuePairs();
                return this.View(input);
            }

            string stringFile = input.ImageName;
            if (input.Image != null)
            {
                stringFile = this.UploadFile(input);
            }

            await this.webEminarService.UpdateAsync(id, input, stringFile);

            return this.RedirectToAction(nameof(this.Details), new { id });
        }
        [Authorize]
        public IActionResult EditWithVideo(int id)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var items = this.context.WebEminars.Where(webEminar => webEminar.AddedByUserId == userId);

            if (items.Any(webEminar => webEminar.AddedByUserId == userId && webEminar.Id == id))
            {
                var inputModel = this.webEminarService.GetById<WebEminarWithVideoViewModel>(id);

                inputModel.CategoriesItems = this.categoriesService.GetAllAsKeyValuePairs();

                return this.View(inputModel);
            }
            else
            {
                throw new ApplicationException();
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> EditWithVideo(int id, WebEminarWithVideoViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.CategoriesItems = this.categoriesService.GetAllAsKeyValuePairs();
                return this.View(input);
            }


            await this.webEminarService.UpdateWithVideoAsync(id, input);

            return this.RedirectToAction(nameof(this.Details), new { id });
        }

        [Authorize]
        public IActionResult IzminalToKachen(int id)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var items = this.context.WebEminars;
            if (items.Any(webEminar => webEminar.AddedByUserId == userId && webEminar.Id == id))
            {
                var inputModel = this.webEminarService.GetById<WebEminarWithVideoViewModel>(id);

                inputModel.CategoriesItems = this.categoriesService.GetAllAsKeyValuePairs();

                return this.View(inputModel);
            }
            else
            {
                throw new ApplicationException();
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> IzminalToKachen(int id, WebEminarWithVideoViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.CategoriesItems = this.categoriesService.GetAllAsKeyValuePairs();
                return this.View(input);
            }


            await this.webEminarService.IzminalToKachen(id, input);

            return this.RedirectToAction(nameof(this.Details), new { id });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var items = this.context.WebEminars;
            if (items.Any(webEminar => webEminar.AddedByUserId == userId && webEminar.Id == id))
            {
                await this.webEminarService.DeleteAsync(id);
            }
            else
            {
                throw new ApplicationException();
            }
            return this.RedirectToAction(nameof(this.All));
        }

        private string UploadFile(WebEminarViewModel vm)
        {
            string fileName = null;
            if (vm.Image != null)
            {
                string uploadDir = Path.Combine(this.WebHostEnviroment.WebRootPath, "Images");
                fileName = Guid.NewGuid().ToString() + "-" + vm.Image.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    vm.Image.CopyTo(fileStream);
                }
            }

            return fileName;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> BookForEvent(int eventId)
        {
            await this.webEminarService.BookEvent(eventId, await this.userManager.GetUserAsync(this.User));
            return this.RedirectToAction("Details", new { id = eventId });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SendToEmail(int id, string content)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var webEminar = this.webEminarService.GetById<WebEminarWithVideoViewModel>(id);
            var html = new StringBuilder();
            html.AppendLine($"<h1>{webEminar.Title}</h1>");
            html.AppendLine($"<h1>От {user.Email}</h1>");
            html.AppendLine($"<h1>{webEminar.CategoryName}</h1>");
            html.AppendLine($"<h1>{content}</h1>");
            html.AppendLine($"<img src=\"{webEminar.ThumbnailUrl}\" />");
            await this.emailSender.SendEmailAsync("hristomir.hari@gmail.com", "WebEminars", webEminar.AddedByUserEmail, webEminar.Title, html.ToString());
            return this.RedirectToAction(nameof(this.Details), new { id });
        }
    }
}
