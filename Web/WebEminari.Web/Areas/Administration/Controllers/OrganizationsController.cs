using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using WebEminari.Services.Data;
using WebEminari.Web.ViewModels.Organizations;

namespace WebEminari.Web.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = "Administrator")]
    public class OrganizationsController : Controller
    {
        private readonly IOrganizationsService organizationsService;

        public OrganizationsController(IOrganizationsService organizationsService)
        {
            this.organizationsService = organizationsService;
        }

        public IActionResult Index()
        {
            var models = this.organizationsService
                .GetAllAsync<OrganizationViewModel>(true);

            return this.View(models);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = this.organizationsService.GetById<OrganizationEditModel>(id, true);

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(OrganizationEditModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.organizationsService.EditAsync(model, true);

            return this.RedirectToAction(nameof(this.Index));
        }

        [HttpPost]
        public async Task<RedirectToActionResult> Delete(int id)
        {
            await this.organizationsService.HardDeleteAsync(id);

            return this.RedirectToAction(nameof(this.Index));
        }
    }
}

