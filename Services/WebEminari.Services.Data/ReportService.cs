using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using WebEminari.Data.Common.Repositories;
using WebEminari.Data.Models;
using WebEminari.Services.Mapping;
using WebEminari.Web.ViewModels.Administration.Report;
using WebEminari.Web.ViewModels.Comments;
using WebEminari.Web.ViewModels.Reports.Comments;
using WebEminari.Web.ViewModels.Reports.WebEminars;

namespace WebEminari.Services.Data
{
    public class ReportsService : IReportsService
    {
        private readonly IDeletableEntityRepository<Report> reportRepository;

        public ReportsService(IDeletableEntityRepository<Report> reportRepository)
            => this.reportRepository = reportRepository;

        public bool Contains(int id)
            => this.reportRepository
                .All()
                .Any(x => x.Id == id);

        public int GetCount()
            => this.reportRepository
                .All()
                .Count();

        public async Task<T> GetReportByIdAsync<T>(int reportId)
            => await this.reportRepository
                .All()
                .Where(x => x.Id == reportId)
                .Include(x => x.WebEminar)
                .Include(x => x.WebEminar.Category)
                .Include(x => x.WebEminar.AddedByUser)
                .Include(x => x.ApplicationUser)
                .Include(x => x.Comment)
                .To<T>()
                .FirstOrDefaultAsync();

        public async Task<IEnumerable<WebEminarReportViewModel>> GetWebEminarReportsCreatedByMeAsync(string userId)
            => await this.reportRepository
                .All()
                .Include(x => x.WebEminar)
                .Include(x => x.WebEminar.Category)
                .Include(x => x.WebEminar.AddedByUser)
                .Where(x => x.ApplicationUserId == userId && x.WebEminarId != null && !x.WebEminar.IsDeleted)
                .To<WebEminarReportViewModel>()
                .ToListAsync();

        public async Task<IEnumerable<CommentReportViewModel>> GetCommentReportsCreatedByMeAsync(string userId)
            => await this.reportRepository
                .All()
                .Include(x => x.Comment)
                .Include(x => x.Comment.ApplicationUser)
                .Where(x => x.ApplicationUserId == userId && x.CommentId != null && !x.Comment.IsDeleted)
                .To<CommentReportViewModel>()
                .ToListAsync();
        public async Task<IEnumerable<T>> GetAllCommentsReportsAsync<T>()
        {
            IQueryable<Report> reports = this.reportRepository.AllWithDeleted().Where(x => x.CommentId != null);

            var reportsMapped = await reports
                .Include(x => x.ApplicationUser)
                .Include(x => x.WebEminar)
                .Include(x => x.Comment)
                .To<T>()
                .ToListAsync();

            return reportsMapped;
        }
        public async Task<IEnumerable<T>> GetAllWebinarsReportsAsync<T>()
        {
            IQueryable<Report> reports = this.reportRepository.AllWithDeleted().Where(x => x.CommentId == null);

            var reportsMapped = await reports
                .Include(x => x.ApplicationUser)
                .Include(x => x.WebEminar)
                .Include(x => x.Comment)
                .To<T>()
                .ToListAsync();

            return reportsMapped;
        }
        public async Task<IEnumerable<T>> GetAllReportsAsync<T>()
        {
            IQueryable<Report> reports = this.reportRepository.AllWithDeleted();

            var reportsMapped = await reports
                .Include(x => x.ApplicationUser)
                .Include(x => x.WebEminar)
                .Include(x => x.Comment)
                .To<T>()
                .ToListAsync();

            return reportsMapped;
        }

        public async Task<IEnumerable<T>> GetAllReportsForCommentsAsync<T>(string userId)
        {
            IQueryable<Report> reports = this.reportRepository.AllAsNoTracking();

            var reportsMapped = await reports
                .Include(x => x.ApplicationUser)
                .Include(x => x.WebEminar)
                .Include(x => x.Comment)
                .Where(x => x.CommentId != null && x.Comment.WebEminar.AddedByUserId == userId)
                .To<T>()
                .ToListAsync();

            return reportsMapped;
        }

        public async Task<IEnumerable<T>> GetAllReportsForWebEminarsAsync<T>(string userId)
        {
            IQueryable<Report> reports = this.reportRepository.AllAsNoTracking();

            var reportsMapped = await reports
                .Include(x => x.ApplicationUser)
                .Include(x => x.WebEminar)
                .Include(x => x.Comment)
                .Where(x => userId == x.WebEminar.AddedByUserId && x.CommentId == null)
                .To<T>()
                .ToListAsync();

            return reportsMapped;
        }

        public async Task CreateWebEminarReportAsync(WebEminarReportInputModel model)
        {
            Report report = new Report
            {
                WebEminarId = model.WebEminarId,
                Subject = model.Subject,
                Description = model.ReportDescription,
                ApplicationUserId = model.ApplicationUserId,
            };

            await this.reportRepository.AddAsync(report);
            await this.reportRepository.SaveChangesAsync();
        }

        public async Task CreateCommentReportAsync(CommentReportInputModel model)
        {
            Report report = new Report
            {
                WebEminarId = model.WebEminarId,
                CommentId = model.CommentId,
                Subject = model.Subject,
                Description = model.ReportDescription,
                ApplicationUserId = model.ApplicationUserId,
            };

            await this.reportRepository.AddAsync(report);
            await this.reportRepository.SaveChangesAsync();
        }

        public async Task EditWebEminarReportAsync(WebEminarReportEditModel model)
        {
            Report entity = this.reportRepository.All().FirstOrDefault(x => x.Id == model.ReportId);
            if (entity != null)
            {
                entity.Subject = model.Subject ?? entity.Subject;
                entity.Description = model.ReportDescription ?? entity.Description;
            }

            await this.reportRepository.SaveChangesAsync();
        }

        public async Task EditCommentReportAsync(CommentReportEditModel model)
        {
            Report entity = this.reportRepository.All().FirstOrDefault(x => x.Id == model.ReportId);
            if (entity != null)
            {
                entity.Subject = model.Subject ?? entity.Subject;
                entity.Description = model.ReportDescription ?? entity.Description;
            }

            await this.reportRepository.SaveChangesAsync();
        }

        public async Task EditReportAdministrationAsync(AdminReportEditModel model)
        {
            Report entity = this.reportRepository.AllWithDeleted().FirstOrDefault(x => x.Id == model.Id);
            if (entity != null)
            {
                entity.Subject = model?.Subject ?? entity.Subject;
                entity.Description = model?.Description ?? entity.Description;

                if (model.IsDeleted)
                {
                    this.reportRepository.Delete(entity);
                }
                else
                {
                    this.reportRepository.Undelete(entity);
                }
            }

            await this.reportRepository.SaveChangesAsync();
        }

        public async Task SoftDeleteReportByIdAsync(int? reportId)
        {
            Report report = this.reportRepository.All().FirstOrDefault(x => x.Id == reportId);

            // todo validate it is marked as deleted but not actually deleted.
            this.reportRepository.Delete(report);
            await this.reportRepository.SaveChangesAsync();
        }

        public async Task HardDeleteReportByIdAsync(int reportId)
        {
            Report report = this.reportRepository.AllWithDeleted().FirstOrDefault(x => x.Id == reportId);
            this.reportRepository.HardDelete(report);
            await this.reportRepository.SaveChangesAsync();
        }
    }
}
