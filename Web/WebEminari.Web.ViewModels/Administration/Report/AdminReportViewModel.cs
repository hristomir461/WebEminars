using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

using WebEminari.Services.Mapping;

namespace WebEminari.Web.ViewModels.Administration.Report
{
    public class AdminReportViewModel : IMapFrom<WebEminari.Data.Models.Report>
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please provide subject.")]
        [MaxLength(300)]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Please provide description.")]
        [MaxLength(500)]
        public string Description { get; set; }

        public string ApplicationUserId { get; set; }

        public string ApplicationUserUserName { get; set; }

        public string ApplicationUserEmail { get; set; }

        public int? WebEminarId { get; set; }

        public string WebEminarTitle { get; set; }

        public string WebEminarDescription { get; set; }

        public int? CommentId { get; set; }

        public string CommentContent { get; set; }

        [Display(Name = "Date created")]
        public DateTime CreatedOn { get; set; }

        [Display(Name = "Is deleted")]
        public bool IsDeleted { get; set; }
    }
}

