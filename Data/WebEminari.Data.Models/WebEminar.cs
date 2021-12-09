using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.RegularExpressions;

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
            this.UserBookings = new HashSet<UserBooking>();
        }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        public string ImageName { get; set; }

        [NotMapped]
        public IFormFile Image { get; set; }

        [Required]
        [MaxLength(600)]
        public string Description { get; set; }

        [MaxLength(50)]
        [Url]
        public string MeetLink { get; set; }

        [Required]
        [BindProperty]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
        public DateTime DateTime { get; set; }

        public string AddedByUserId { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public int MaxPeople { get; set; }

        [Url]
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

        public string ThumbnailUrl => $"https://i.ytimg.com/vi/{this.VideoId}/mqdefault.jpg";

        public virtual ApplicationUser AddedByUser { get; set; }

        public virtual ICollection<Vote> Votes { get; set; }

        public virtual ICollection<UserBooking> UserBookings { get; set; }

    }
}

