using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebEminari.Web.ViewModels.WebEminars
{
   public class WebEminarInputModel
    {
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
        [MaxLength(30)]
        public string MeetLink { get; set; }

        [Required]
        [BindProperty]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
        public DateTime DateTime { get; set; }

    }
}
