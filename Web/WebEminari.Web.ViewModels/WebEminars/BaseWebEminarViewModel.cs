using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;

using AutoMapper;

using Ganss.XSS;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using WebEminari.Data.Models;
using WebEminari.Services.Mapping;

namespace WebEminari.Web.ViewModels.WebEminars
{
   public class BaseWebEminarViewModel : IMapFrom<WebEminar>
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
        [DataType(DataType.Html)]
        public string Description { get; set; }

        public string SearchString { get; set; }

        public string AddedByUserEmail { get; set; }

        public string AddedByUserFirstName { get; set; }

        public string AddedByUserImagePath { get; set; }

        public string AddedByUserLastName { get; set; }

        public double AverageVote { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }
    }
}
