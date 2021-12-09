﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.RegularExpressions;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebEminari.Web.ViewModels.WebEminars
{
   public class BaseWebEminarViewModel 
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Too long title")]
        [MinLength(4, ErrorMessage = "Too short title")]
        public string Title { get; set; }

        public string ImageName { get; set; }

        public string Video { get; set; }
    
        [NotMapped]
        public IFormFile Image { get; set; }

        [Required]
        [MaxLength(600)]
        [MinLength(80, ErrorMessage = "Too short description")]
        public string Description { get; set; }

        public string SearchString { get; set; }

        public string AddedByUserEmail { get; set; }

        public string AddedByUserFirstName { get; set; }

        public string AddedByUserLastName { get; set; }

        public double AverageVote { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }
    }
}
