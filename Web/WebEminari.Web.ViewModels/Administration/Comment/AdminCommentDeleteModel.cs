using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

using WebEminari.Data.Models;
using WebEminari.Services.Mapping;

namespace WebEminari.Web.ViewModels
{
    public class AdminCommentDeleteModel : IMapFrom<Data.Models.Comment>
    {
        public int Id { get; set; }

        [Display(Name = "Content")]
        [Required(ErrorMessage = "Please, provide content between 0 and 1000 characters.")]
        [MaxLength(300, ErrorMessage = "Please, provide content between 0 and 1000 characters.")]
        public string Content { get; set; }

        [Display(Name = "Is deleted")]
        public bool IsDeleted { get; set; }

        public string ApplicationUserId { get; set; }

        public string ApplicationUserUserName { get; set; }

        public int WebEminarId { get; set; }

        [Display(Name = "Date created")]
        public DateTime CreatedOn { get; set; }

        public DateTime WebEminarCreatedOn { get; set; }

        public string WebEminarTitle { get; set; }

        public string WebEminarDescription { get; set; }

        public string WebEminarCategoryName { get; set; }

        public string WebEminarAddedByUserUserName { get; set; }
    }
}