using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

using AutoMapper;

using WebEminari.Services.Mapping;

using Xunit;

namespace WebEminari.Web.ViewModels.Reports.WebEminars
{
    public class WebEminarReportInputModel : IMapFrom<Data.Models.WebEminar>, IMapTo<Data.Models.Report>, IHaveCustomMappings
    {
        public int WebEminarId { get; set; }

        public int ReportId { get; set; }

        public string WebEminarTitle { get; set; }

        public DateTime CreatedOn { get; set; }

        public string WebEminarApplicationUserUserName { get; set; }

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

        public string ApplicationUserId { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<WebEminarReportInputModel, Data.Models.Report>()
                .ForMember(
                    dest => dest.Description,
                    opt => opt.MapFrom(src => src.ReportDescription));

            configuration.CreateMap<Data.Models.WebEminar, WebEminarReportInputModel>()
                .ForMember(
                    dest => dest.WebEminarId,
                    opt => opt.MapFrom(src => src.Id))
                .ForMember(
                    dest => dest.WebEminarTitle,
                    opt => opt.MapFrom(src => src.Title))
                .ForMember(
                    dest => dest.WebEminarDescription,
                    opt => opt.MapFrom(src => src.Description))
                .ForMember(
                    dest => dest.WebEminarCreatedOn,
                    opt => opt.MapFrom(src => src.CreatedOn))
                .ForMember(
                    dest => dest.WebEminarCategoryName,
                    opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(
                    dest => dest.WebEminarApplicationUserUserName,
                    opt => opt.MapFrom(src => src.AddedByUser.UserName));
        }
    }
}

