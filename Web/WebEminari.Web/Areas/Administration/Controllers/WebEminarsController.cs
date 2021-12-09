using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

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
    public class WebEminarsController : Controller
    {
        private readonly IDeletableEntityRepository<WebEminar> dataRepository;

        private readonly ICategoriesService categoriesService;

        public IWebHostEnvironment WebHostEnviroment { get; }

        private readonly UserManager<ApplicationUser> userManager;

        private readonly IWebEminarsService webEminarService;

        private readonly ApplicationDbContext context;

        public WebEminarsController(IDeletableEntityRepository<WebEminar> dataRepository, ICategoriesService categoriesService, IWebEminarsService webEminarService, ApplicationDbContext context, UserManager<ApplicationUser> userManager, IWebHostEnvironment webHostEnviroment)
        {
            this.userManager = userManager;
            this.context = context;
            this.WebHostEnviroment = webHostEnviroment;
            this.webEminarService = webEminarService;
            this.categoriesService = categoriesService;
            this.dataRepository = dataRepository;
        }
     

        // GET: Administration/WebEminars
        public async Task<IActionResult> Index()
        {
            return this.View(await this.dataRepository
               .All()
               .ToListAsync());
        }

        public IActionResult Details(int id)
        {
            var webEminar = this.webEminarService.GetById<WebEminarViewModel>(id);

            return this.View(webEminar);
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

            return this.Redirect("All");
        }
        public IActionResult Edit(int id)
        {
                var inputModel = this.webEminarService.GetById<WebEminarViewModel>(id);

                inputModel.CategoriesItems = this.categoriesService.GetAllAsKeyValuePairs();

                return this.View(inputModel);
        }

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


        // GET: Administration/WebEminars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {

            if (id == null)
            {
                return this.NotFound();
            }

            var webEminar = await this.dataRepository
                .All()
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
              .All()
              .FirstOrDefault(x => x.Id == id);

            this.dataRepository.Delete(webEminar);

            await this.dataRepository.SaveChangesAsync();

            return this.RedirectToAction(nameof(this.Index)); ;
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
