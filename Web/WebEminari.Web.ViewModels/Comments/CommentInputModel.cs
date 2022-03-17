using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

using WebEminari.Data.Models;
using WebEminari.Services.Mapping;

namespace WebEminari.Web.ViewModels.Comments
{
   public class CommentInputModel : IMapTo<Data.Models.Comment>
    {
        public int WebEminarId { get; set; }

        [MaxLength(300, ErrorMessage = "Please, provide content between 0 and 1000 characters.")]
        public string Content { get; set; }

        // public string ApplicationUserUserName { get; set; }
        public string ApplicationUserId { get; set; }

        public int? ParentId { get; set; }

    }
}
