using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using AutoMapper;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using WebEminari.Data.Models;
using WebEminari.Services.Mapping;

namespace WebEminari.Web.ViewModels.WebEminars
{
    public class WebEminarViewModel : BaseWebEminarViewModel, IMapFrom<WebEminar>, IHaveCustomMappings
    {
        public WebEminarViewModel()
        {
            this.Video = "default";
        }
        public int Id { get; set; }

        public ICollection<string> UsersBooked { get; set; }

        public ICollection<string> UsersNamesBooked { get; set; }

        [Required]
        public int MaxPeople { get; set; }

        [Required(ErrorMessage = "Immage is mandatory!")]
        public IFormFile Image { get; set; }

        [Required(ErrorMessage = "Url is required")]
        [MaxLength(50)]
        [Url]
        public string MeetLink { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        public IEnumerable<KeyValuePair<string, string>> CategoriesItems { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public string Video { get; set; }

        public static string GetYouTubeId(string url)
        {
            var regex = @"(?:youtube\.com\/(?:[^\/]+\/.+\/|(?:v|e(?:mbed)?|watch)\/|.*[?&amp;]v=)|youtu\.be\/)([^""&amp;?\/ ]{11})";

            var match = Regex.Match(url, regex);

            if (match.Success)
            {
                return match.Groups[1].Value;
            }

            return url;
        }

        private string VideoId => GetYouTubeId(this.Video);

        public string VideoUrl => $"https://www.youtube.com/embed/{this.VideoId}";

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<WebEminar, WebEminarViewModel>()
                   .ForMember(x => x.AverageVote, opt =>
                    opt.MapFrom(x => x.Votes.Count == 0 ? 0 : x.Votes.Average(v => v.Value)))
                   .ForMember(x => x.UsersBooked, opt =>
                      opt.MapFrom(x => x.UserBookings.Select(y => y.User.UserName)))
                    .ForMember(x => x.UsersNamesBooked, opt =>
                      opt.MapFrom(x => x.UserBookings.Select(y => y.User.FirstName + y.User.LastName)));
        }
    }
}
