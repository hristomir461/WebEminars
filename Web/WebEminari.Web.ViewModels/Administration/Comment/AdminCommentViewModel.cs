using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

using AutoMapper;

using WebEminari.Data.Models;
using WebEminari.Services.Mapping;

namespace WebEminari.Web.ViewModels.Administration
{
    public class AdminCommentViewModel : IMapFrom<Data.Models.Comment>, IHaveCustomMappings
    {
        public string ApplicationUserUserName { get; set; }

        public string ApplicationUserEmail { get; set; }

        [Display(Name = "Is deleted")]
        public bool IsDeleted { get; set; }

        public int CommentId { get; set; }

        public int WebEminarId { get; set; }

        [Display(Name = "Content")]
        [Required(ErrorMessage = "Please, provide content between 0 and 1000 characters.")]
        [MaxLength(300, ErrorMessage = "Please, provide content between 0 and 1000 characters.")]
        public string Content { get; set; }

        [Display(Name = "Date created")]
        public DateTime CreatedOn { get; set; }

        public string ApplicationUserId { get; set; }

        public DateTime WebEminarCreatedOn { get; set; }

        public string WebEminarTitle { get; set; }

        public string WebEminarDescription { get; set; }

        public string WebEminarCategoryName { get; set; }

        public string WebEminarApplicationUserUserName { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Data.Models.Comment, AdminCommentViewModel>()
                .ForMember(
                    dest => dest.CommentId,
                    opt =>
                        opt.MapFrom(src => src.Id));
        }
    }
}
