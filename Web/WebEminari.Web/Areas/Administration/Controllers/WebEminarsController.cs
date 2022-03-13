using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebEminari.Data;
using WebEminari.Data.Common.Repositories;
using WebEminari.Data.Models;
using WebEminari.Services.Data;
using WebEminari.Web.ViewModels.WebEminars;

namespace WebEminari.Web.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = "Administrator")]
    public class WebEminarsController : Controller
    {
        private readonly IDeletableEntityRepository<WebEminar> dataRepository;

        private readonly IDeletableEntityRepository<UserBooking> userBookingsRepository;

        private readonly IDeletableEntityRepository<Vote> votesRepository;

        private readonly IDeletableEntityRepository<Comment> commentsRepository;

        private readonly IDeletableEntityRepository<Report> reportsRepository;

        private readonly IDeletableEntityRepository<Like> likesRepository;

        private readonly ICategoriesService categoriesService;

        public IWebHostEnvironment WebHostEnviroment { get; }

        private readonly UserManager<ApplicationUser> userManager;

        private readonly IWebEminarsService webEminarService;

        private readonly ApplicationDbContext context;

        private readonly IInformationService informationService;

        public WebEminarsController(IInformationService informationService, IDeletableEntityRepository<Like> likesRepository, IDeletableEntityRepository<Report> reportsRepository, IDeletableEntityRepository<Comment> commentsRepository, IDeletableEntityRepository<Vote> votesRepository, IDeletableEntityRepository<UserBooking> userBookingsRepository, IDeletableEntityRepository<WebEminar> dataRepository, ICategoriesService categoriesService, IWebEminarsService webEminarService, ApplicationDbContext context, UserManager<ApplicationUser> userManager, IWebHostEnvironment webHostEnviroment)
        {
            this.likesRepository = likesRepository;
            this.reportsRepository = reportsRepository;
            this.commentsRepository = commentsRepository;
            this.votesRepository = votesRepository;
            this.userBookingsRepository = userBookingsRepository;
            this.userManager = userManager;
            this.context = context;
            this.WebHostEnviroment = webHostEnviroment;
            this.webEminarService = webEminarService;
            this.categoriesService = categoriesService;
            this.dataRepository = dataRepository;
            this.informationService = informationService;
        }


        // GET: Administration/WebEminars
        public async Task<IActionResult> Index()
        {
           
            var model = await this.informationService.GenerateAdministrationIndexViewModel();

            return this.View(model);
        }
        public async Task<IActionResult> IndexWebEminars()
        {
            return this.View(await this.dataRepository
               .AllWithDeleted()
               .Where(webEminars => webEminars.ImageName != null)
               .ToListAsync());
        }
        public async Task<IActionResult> IndexWebEminarsWithVideos()
        {
            return this.View(await this.dataRepository
               .AllWithDeleted()
               .Where(webEminars => webEminars.ImageName == null)
               .ToListAsync());
        }
        public IActionResult BookedPeople(int id)
        {
            var webEminar = this.webEminarService.GetById<WebEminarViewModel>(id);
            var userBookings = context.UserBookings.Where(x => x.WebEminarId == webEminar.Id);
            return this.View(userBookings);
        }

        // GET: Administration/WebEminars/Create
        public IActionResult Create()
        {
            var viewModel = new WebEminarViewModel();

            viewModel.CategoriesItems = this.categoriesService.GetAllAsKeyValuePairs();

            return this.View(viewModel);
        }

        [HttpPost]
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
        public IActionResult CreateWithVideo()
        {
            var viewModel = new WebEminarWithVideoViewModel();

            viewModel.CategoriesItems = this.categoriesService.GetAllAsKeyValuePairs();

            return this.View(viewModel);
        }

        [HttpPost]
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

        public IActionResult Edit(int id)
        {
       
                var inputModel = this.webEminarService.GetByIdAdmin<WebEminarViewModel>(id);

                inputModel.CategoriesItems = this.categoriesService.GetAllAsKeyValuePairs();

                return this.View(inputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, WebEminarViewModel input)
        {
     
            string stringFile = input.ImageName;
            if (input.Image != null)
            {
                stringFile = this.UploadFile(input);
            }

            await this.webEminarService.UpdateAdminAsync(id, input, stringFile);

            return this.RedirectToAction("IndexWebEminars");
        }
        public IActionResult EditWithVideo(int id)
        {

                var inputModel = this.webEminarService.GetById<WebEminarWithVideoViewModel>(id);

                inputModel.CategoriesItems = this.categoriesService.GetAllAsKeyValuePairs();

                return this.View(inputModel);

        }

        [HttpPost]
        public async Task<IActionResult> EditWithVideo(int id, WebEminarWithVideoViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.CategoriesItems = this.categoriesService.GetAllAsKeyValuePairs();
                return this.View(input);
            }


            await this.webEminarService.UpdateWithVideoAsync(id, input);

            return this.RedirectToAction("IndexWebEminarsWithVideos");
        }
        public IActionResult IzminalToKachen(int id)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var items = this.context.WebEminars;
            if (items.Any(webEminar => webEminar.AddedByUserId == userId))
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

        [HttpPost]
        public async Task<IActionResult> IzminalToKachen(int id, WebEminarWithVideoViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.CategoriesItems = this.categoriesService.GetAllAsKeyValuePairs();
                return this.View(input);
            }


            await this.webEminarService.IzminalToKachen(id, input);

            return this.RedirectToAction("IndexWebEminarsWithVideos");
        }

        // GET: Administration/WebEminars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {

            if (id == null)
            {
                return this.NotFound();
            }

            var webEminar = await this.dataRepository
                .AllWithDeleted()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (webEminar == null)
            {
                return this.NotFound();
            }

            return this.View(webEminar);
        }

        // POST: Administration/WebEminars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var webEminar = this.dataRepository
              .AllWithDeleted()
              .FirstOrDefault(x => x.Id == id);
            var userBookings = this.context.UserBookings.Where(x => x.WebEminarId == id).ToList();
            var votes = this.context.Votes.Where(x => x.WebEminarId == id).ToList();
            var comments = this.context.Comments.Where(x => x.WebEminarId == id).ToList();
            var reports = this.context.Reports.Where(x => x.WebEminarId == id).ToList();
            var likes = this.context.Likes.Where(x => x.WebEminarId == id).ToList();
            foreach (var item in reports)
            {
                this.reportsRepository.HardDelete(item);
            }
            foreach (var item in comments)
            {
                this.commentsRepository.HardDelete(item);
            }
            foreach (var item in votes)
            {
                this.votesRepository.HardDelete(item);
            }
            foreach (var item in userBookings)
            {
                this.userBookingsRepository.HardDelete(item);
            }
            foreach (var item in likes)
            {
                this.likesRepository.HardDelete(item);
            }
            this.dataRepository.HardDelete(webEminar);

            await this.dataRepository.SaveChangesAsync();

            return this.RedirectToAction(nameof(this.Index));
        }

        private bool WebEminarExists(int id)
        {
            return this.dataRepository
               .All()
               .Any(e => e.Id == id);
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
    }
}
