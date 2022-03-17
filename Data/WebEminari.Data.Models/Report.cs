using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

using WebEminari.Data.Common.Models;

namespace WebEminari.Data.Models
{
    public class Report : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(300)]
        public string Subject { get; set; }

        [Required]
        [MaxLength(300)]
        public string Description { get; set; }

        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public int? WebEminarId { get; set; }

        public WebEminar WebEminar { get; set; }

        public int? CommentId { get; set; }

        public Comment Comment { get; set; }
    }
}