using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

using AutoMapper;

using WebEminari.Services.Mapping;

namespace WebEminari.Web.ViewModels.Reports.WebEminars
{
    public class WebEminarReportViewModel : IMapFrom<Data.Models.Report>, IHaveCustomMappings
    {
        public int WebEminarId { get; set; }

        public int ReportId { get; set; }

        public string WebEminarTitle { get; set; }

        public string WebEminarAddedByUserUserName { get; set; }

        public string WebEminarDescription { get; set; }

        public string WebEminarCategoryName { get; set; }

        public string CreatedByUserName { get; set; }

        public DateTime WebEminarCreatedOn { get; set; }

        [Required(ErrorMessage = "Please provide subject to the report.")]
        [MaxLength(300)]
        public string Subject { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Please provide description to the report.")]
        [MaxLength(300)]
        public string ReportDescription { get; set; }

        public string ReportingUserId { get; set; }

        public DateTime CreatedOn { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Data.Models.Report, WebEminarReportViewModel>()
                .ForMember(dest => dest.ReportId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ReportDescription, opt => opt.MapFrom(src => src.Description));
        }
    }
}
