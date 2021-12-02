using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

using AutoMapper;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using WebEminari.Data.Models;
using WebEminari.Services.Mapping;

namespace WebEminari.Web.ViewModels.WebEminars
{
    public class WebEminarViewModel : IMapFrom<WebEminar>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string CategoryName { get; set; }

        public string ImageName { get; set; }

        public int MaxPeople { get; set; }

        public IFormFile Image { get; set; }

        public string Description { get; set; }

        public string MeetLink { get; set; }

        public DateTime DateTime { get; set; }

        public string AddedByUserEmail { get; set; }

        public string AddedByUserFirstName { get; set; }

        public string AddedByUserLastName { get; set; }

        public double AverageVote { get; set; }

        public int CategoryId { get; set; }

        public IEnumerable<KeyValuePair<string, string>> CategoriesItems { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<WebEminar, WebEminarViewModel>()
                   .ForMember(x => x.AverageVote, opt =>
                    opt.MapFrom(x => x.Votes.Count == 0 ? 0 : x.Votes.Average(v => v.Value)));
        }
    }
}
