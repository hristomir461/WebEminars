using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using WebEminari.Data;
using WebEminari.Services.Data;
using WebEminari.Web.ViewModels.Administration.Report;
using WebEminari.Web.ViewModels.Reports.Comments;
using WebEminari.Web.ViewModels.Reports.WebEminars;

namespace WebEminari.Web.Controllers
{
    public class ReportController : BaseController
    {
        private readonly IWebEminarsService lessonsService;
        private readonly ICommentsService commentsService;
        private readonly IReportsService reportsService;
        private readonly ApplicationDbContext context;

        public ReportController(
            IWebEminarsService lessonsService,
            ICommentsService commentsService,
            IReportsService reportsService,
            ApplicationDbContext context)
        {
            this.lessonsService = lessonsService;
            this.commentsService = commentsService;
            this.reportsService = reportsService;
            this.context = context;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> CommentsReports()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            IEnumerable<AdminReportViewModel> reports = await this.reportsService.GetAllReportsForCommentsAsync<AdminReportViewModel>(userId);

            return this.View(reports);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> WebEminarsReports()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            IEnumerable<AdminReportViewModel> reports = await this.reportsService.GetAllReportsForWebEminarsAsync<AdminReportViewModel>(userId);

            return this.View(reports);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> WebEminarReportsCreatedByMe()
        {
            IEnumerable<WebEminarReportViewModel> models = await this.reportsService.GetWebEminarReportsCreatedByMeAsync(this.GetUserId());
            return this.View(models);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> WebEminar(int id)
        {
            if (!this.lessonsService.Contains(id))
            {
                this.Response.StatusCode = 404;
                return this.NotFound();
            }

            WebEminarReportInputModel lessonToReport = this.lessonsService.GetById<WebEminarReportInputModel>(id);
            return this.View(lessonToReport);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> WebEminar(WebEminarReportInputModel model)
        {
            model.ApplicationUserId = this.GetUserId();

            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            await this.reportsService.CreateWebEminarReportAsync(model);
            return this.RedirectToAction(nameof(this.WebEminarReportsCreatedByMe));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> WebEminarEdit(int reportId)
        {
            if (!this.reportsService.Contains(reportId))
            {
                this.Response.StatusCode = 404;
                return this.NotFound();
            }

            WebEminarReportEditModel model = await this.reportsService.GetReportByIdAsync<WebEminarReportEditModel>(reportId);
            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> WebEminarEdit(WebEminarReportEditModel model)
        {
            if (!this.reportsService.Contains(model.ReportId))
            {
                this.Response.StatusCode = 404;
                return this.NotFound();
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.reportsService.EditWebEminarReportAsync(model);
            return this.RedirectToAction(nameof(this.WebEminarReportsCreatedByMe));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> WebEminarDelete(int reportId)
        {
            if (!this.reportsService.Contains(reportId))
            {
                this.Response.StatusCode = 404;
                return this.NotFound();
            }

            WebEminarReportDeleteModel model = await this.reportsService.GetReportByIdAsync<WebEminarReportDeleteModel>(reportId);
            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> WebEminarDelete(WebEminarReportDeleteModel model)
        {
            if (!this.reportsService.Contains(model.ReportId))
            {
                this.Response.StatusCode = 404;
                return this.NotFound();
            }
            
         
             await this.reportsService.SoftDeleteReportByIdAsync(model.ReportId);
             return this.RedirectToAction(nameof(this.WebEminarReportsCreatedByMe));
        }

        [Authorize]
        public async Task<IActionResult> CommentReportsCreatedByMe()
        {
            IEnumerable<CommentReportViewModel> models = await this.reportsService.GetCommentReportsCreatedByMeAsync(this.GetUserId());
            return this.View(models);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Comment(int id)
        {
            if (!this.commentsService.Contains(id))
            {
                this.Response.StatusCode = 404;
                return this.NotFound();
            }

            CommentReportInputModel model = await this.commentsService.GetCommentByIdWithDeletedAsync<CommentReportInputModel>(id);
            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Comment(CommentReportInputModel model)
        {
            model.ApplicationUserId = this.GetUserId();

            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            await this.reportsService.CreateCommentReportAsync(model);
            return this.RedirectToAction(nameof(this.CommentReportsCreatedByMe));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> CommentEdit(int reportId)
        {
            if (!this.reportsService.Contains(reportId))
            {
                this.Response.StatusCode = 404;
                return this.NotFound();
            }

            CommentReportEditModel model = await this.reportsService.GetReportByIdAsync<CommentReportEditModel>(reportId);
            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CommentEdit(CommentReportEditModel model)
        {
            if (!this.reportsService.Contains(model.ReportId))
            {
                this.Response.StatusCode = 404;
                return this.NotFound();
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

   
            await this.reportsService.EditCommentReportAsync(model);
            return this.RedirectToAction(nameof(this.CommentReportsCreatedByMe));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> CommentDelete(int reportId)
        {
            if (!this.reportsService.Contains(reportId))
            {
                this.Response.StatusCode = 404;
                return this.NotFound();
            }

            CommentReportDeleteModel model = await this.reportsService.GetReportByIdAsync<CommentReportDeleteModel>(reportId);
            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CommentDelete(CommentReportDeleteModel model)
        {
            if (!this.reportsService.Contains(model.ReportId))
            {
                this.Response.StatusCode = 404;
                return this.NotFound();
            }

            var items = this.context.WebEminars;
            if (!items.Any(webEminar => webEminar.AddedByUserId == this.GetUserId()))
            {
                return this.View(nameof(this.Unauthorized));
            }
          


            await this.reportsService.SoftDeleteReportByIdAsync(model.ReportId);
            return this.RedirectToAction(nameof(this.CommentReportsCreatedByMe));
        }
    }
}
