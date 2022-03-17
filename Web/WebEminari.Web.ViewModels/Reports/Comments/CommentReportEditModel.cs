using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

using AutoMapper;

using WebEminari.Services.Mapping;

namespace WebEminari.Web.ViewModels.Reports.Comments
{
    public class CommentReportEditModel : IMapFrom<WebEminari.Data.Models.Report>, IHaveCustomMappings
    {
        public int WebEminarId { get; set; }

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

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Data.Models.Report, CommentReportEditModel>()
                .ForMember(dest => dest.ReportId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ReportDescription, opt => opt.MapFrom(src => src.Description));
            configuration.CreateMap<Data.Models.WebEminar, CommentReportEditModel>()
           .ForMember(
               dest => dest.WebEminarId,
               opt => opt.MapFrom(src => src.Id));
        }
    }
}
