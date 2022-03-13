using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

using AutoMapper;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using WebEminari.Common;
using WebEminari.Data.Models;
using WebEminari.Services.Mapping;
using WebEminari.Web.ViewModels.Comments;

namespace WebEminari.Web.ViewModels.WebEminars
{
    public class WebEminarViewModel : BaseWebEminarViewModel, IMapFrom<WebEminar>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public ICollection<string> UsersBooked { get; set; }

        public ICollection<string> UsersNamesBooked { get; set; }

        public ICollection<UserBooking> UsersBookings { get; set; }

        public int LikesCount { get; set; }

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

        private string VideoId => GlobalMethods.GetYouTubeVideoIdFromUrl(this.Video);

        public string VideoUrl => $"https://www.youtube.com/embed/{this.VideoId}";

        public string ThumbnailUrl => $"https://i.ytimg.com/vi/{this.VideoId}/maxresdefault.jpg";

        public IEnumerable<Comment> Comments { get; set; }

        public bool IsDeleted { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<WebEminar, WebEminarViewModel>()
                   .ForMember(x => x.AverageVote, opt => opt.MapFrom(x => x.Votes.Count == 0 ? 0 : x.Votes.Average(v => v.Value)))
                   .ForMember(x => x.UsersBooked, opt => opt.MapFrom(x => x.UserBookings.Select(y => y.User.UserName)));
        }
    }
}
