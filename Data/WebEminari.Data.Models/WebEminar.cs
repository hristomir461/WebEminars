using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using WebEminari.Data.Common.Models;

namespace WebEminari.Data.Models
{
   public class WebEminar : BaseDeletableModel<int>
    {

        public WebEminar()
        {
            this.Votes = new HashSet<Vote>();
        }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        public string ImageName { get; set; }

        [NotMapped]
        public IFormFile Image { get; set; }

        [Required]
        [MaxLength(600)]
        public string Description { get; set; }

        [Required]
        [MaxLength(50)]
        public string MeetLink { get; set; }

        [Required]
        [BindProperty]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
        public DateTime DateTime { get; set; }

        public string AddedByUserId { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public int MaxPeople { get; set; }

        public virtual ApplicationUser AddedByUser { get; set; }

        public virtual ICollection<Vote> Votes { get; set; }

    }
}
