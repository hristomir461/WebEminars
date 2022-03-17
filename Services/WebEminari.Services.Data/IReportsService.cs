using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WebEminari.Web.ViewModels.Administration.Report;
using WebEminari.Web.ViewModels.Reports.Comments;
using WebEminari.Web.ViewModels.Reports.WebEminars;

namespace WebEminari.Services.Data
{
    public interface IReportsService
    {
        int GetCount();

        bool Contains(int id);

        Task<T> GetReportByIdAsync<T>(int reportId);

        Task<IEnumerable<T>> GetAllWebinarsReportsAsync<T>();

        Task<IEnumerable<T>> GetAllCommentsReportsAsync<T>();

        Task<IEnumerable<T>> GetAllReportsAsync<T>();

        Task<IEnumerable<T>> GetAllReportsForCommentsAsync<T>(string userId);

        Task<IEnumerable<T>> GetAllReportsForWebEminarsAsync<T>(string userId);

        Task<IEnumerable<WebEminarReportViewModel>> GetWebEminarReportsCreatedByMeAsync(string userId);

        Task<IEnumerable<CommentReportViewModel>> GetCommentReportsCreatedByMeAsync(string userId);

        Task CreateWebEminarReportAsync(WebEminarReportInputModel model);

        Task CreateCommentReportAsync(CommentReportInputModel model);

        Task EditCommentReportAsync(CommentReportEditModel commentReportModel);

        Task EditWebEminarReportAsync(WebEminarReportEditModel model);

        Task EditReportAdministrationAsync(AdminReportEditModel model);

        Task SoftDeleteReportByIdAsync(int? reportId);

        Task HardDeleteReportByIdAsync(int reportId);
    }
}
