using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

using WebEminari.Data.Common.Models;

namespace WebEminari.Data.Models
{
    public class Comment : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(300)]
        public string Content { get; set; }

        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public int WebEminarId { get; set; }

        public int? ParentId { get; set; }

        public virtual Comment Parent { get; set; }

        public WebEminar WebEminar { get; set; }
    }
}
