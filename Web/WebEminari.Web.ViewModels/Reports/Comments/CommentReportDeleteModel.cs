using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

using AutoMapper;

using WebEminari.Data.Models;
using WebEminari.Services.Mapping;

namespace WebEminari.Web.ViewModels.Reports.Comments
{
    public class CommentReportDeleteModel : IMapFrom<WebEminari.Data.Models.Report>, IHaveCustomMappings
    {
        public int CommentId { get; set; }

        public int ReportId { get; set; }

        public string CommentApplicationUserUserName { get; set; }

        public string CommentContent { get; set; }

        public DateTime CommentCreatedOn { get; set; }

        [MaxLength(300)]
        [Required(ErrorMessage = "Please provide subject to the report.")]
        public string Subject { get; set; }

        [MaxLength(300)]
        [Display(Name = "Description")]
        [Required(ErrorMessage = "Please provide description to the report.")]
        public string ReportDescription { get; set; }

        public string ApplicationUserId { get; set; }

        public string ApplicationUserUserName { get; set; }

        public DateTime CreatedOn { get; set; }

        public string WebEminar { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Data.Models.Report, CommentReportDeleteModel>()
                .ForMember(dest => dest.ReportId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ReportDescription, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.WebEminar, opt => opt.MapFrom(src => src.WebEminar.AddedByUserId));
        }

    }
}
