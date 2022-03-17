using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

using WebEminari.Data.Common.Models;

namespace WebEminari.Data.Models
{
    public class Like : BaseDeletableModel<int>
    {
        public int WebEminarId { get; set; }

        public WebEminar WebEminar { get; set; }

        [Required]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
