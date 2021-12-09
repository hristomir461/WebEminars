using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

using WebEminari.Data.Models;
using WebEminari.Services.Mapping;

namespace WebEminari.Web.ViewModels.WebEminars
{
    public class WebEminarsInListViewModel : IMapFrom<WebEminar>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string MeetLink { get; set; }

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
        public string VideoId => GetYouTubeId(this.Video);
        public string VideoUrl => $"https://www.youtube.com/embed/{this.VideoId}";

        public string ThumbnailUrl => $"https://i.ytimg.com/vi/{this.VideoId}/mqdefault.jpg";

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public string AddedByUserId { get; set; }

        public string ImageName { get; set; }

        public string Description { get;set; }

        public DateTime DateTime { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }
    }
}
