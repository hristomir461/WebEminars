using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

using WebEminari.Services.Mapping;

namespace WebEminari.Web.ViewModels.Comments
{
    public class CommentEditModel : IMapFrom<Data.Models.Comment>
    {
        public int Id { get; set; }

        public int WebEminarId { get; set; }

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
    }
}
