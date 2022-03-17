using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using WebEminari.Services.Data;
using WebEminari.Web.ViewModels.Administration.Report;
using WebEminari.Web.ViewModels.HelperModels;

namespace WebEminari.Web.Areas.Administration.Controllers
{
    public class ReportsController : AdministrationController
    {
        private readonly IReportsService reportsService;

        public ReportsController(IReportsService reportsService)
            => this.reportsService = reportsService;

        [HttpGet]
        public async Task<IActionResult> IndexComments()
        {
            IEnumerable<AdminReportViewModel> reports = await this.reportsService.GetAllCommentsReportsAsync<AdminReportViewModel>();

            return this.View(reports);
        }
        [HttpGet]
        public async Task<IActionResult> IndexWebEminars()
        {
            IEnumerable<AdminReportViewModel> reports = await this.reportsService.GetAllWebinarsReportsAsync<AdminReportViewModel>();

            return this.View(reports);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            IEnumerable<AdminReportEditModel> reports = await this.reportsService.GetAllReportsAsync<AdminReportEditModel>();
            AdminReportEditModel report = reports.FirstOrDefault(x => x.Id == id);
            return this.View(report);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AdminReportEditModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.reportsService.EditReportAdministrationAsync(model);
            if (model.CommentId != null)
            {
                return this.RedirectToAction(nameof(this.IndexComments));
            }
            else 
            {
                return this.RedirectToAction(nameof(this.IndexWebEminars));
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            IEnumerable<AdminReportDeleteModel> reports = await this.reportsService.GetAllReportsAsync<AdminReportDeleteModel>();
            AdminReportDeleteModel report = reports.FirstOrDefault(x => x.Id == id);
            return this.View(report);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            IEnumerable<AdminReportDeleteModel> reports = await this.reportsService.GetAllReportsAsync<AdminReportDeleteModel>();
            await this.reportsService.HardDeleteReportByIdAsync(id);
            AdminReportDeleteModel report = reports.FirstOrDefault(x => x.Id == id);
            if (report.CommentId != null)
            {
                return this.RedirectToAction(nameof(this.IndexComments));
            }
            else
            {
                return this.RedirectToAction(nameof(this.IndexWebEminars));
            }
        }
    }
}

