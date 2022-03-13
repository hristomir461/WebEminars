using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using AutoMapper;

using WebEminari.Data.Models;
using WebEminari.Data.Common;
using WebEminari.Services.Mapping;
using WebEminari.Common;

namespace WebEminari.Web.ViewModels.WebEminars
{
    public class WebEminarWithVideoViewModel : BaseWebEminarViewModel, IMapFrom<WebEminar>, IHaveCustomMappings
    {
        public int Id { get; set; }


        [Required]
        public string Video { get; set; }

        private string VideoId =>  GlobalMethods.GetYouTubeVideoIdFromUrl(this.Video);

        public string VideoUrl => $"https://www.youtube.com/embed/{this.VideoId}";

        public string ThumbnailUrl => $"https://i.ytimg.com/vi/{this.VideoId}/mqdefault.jpg";

        public IEnumerable<KeyValuePair<string, string>> CategoriesItems { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<WebEminar, WebEminarViewModel>()
                   .ForMember(x => x.AverageVote, opt =>
                    opt.MapFrom(x => x.Votes.Count == 0 ? 0 : x.Votes.Average(v => v.Value)))
                   .ForMember(x => x.UsersBooked, opt =>
                      opt.MapFrom(x => x.UserBookings.Select(y => y.User.UserName)));
        }
    }
}
