using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using AutoMapper;

using WebEminari.Common;
using WebEminari.Data.Models;
using WebEminari.Services.Mapping;

namespace WebEminari.Web.ViewModels.Home
{
    public class IndexPageWebEminarViewModel : IMapFrom<WebEminar>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public double AverageVote { get; set; }

        public string CategoryName { get; set; }

        public string ImageName { get; set; }

        public string Video { get; set; }

        public int CommentsCount { get; set; }
        public int LikesCount { get; set; }

        public string VideoId => GlobalMethods.GetYouTubeVideoIdFromUrl(this.Video);
        public string VideoUrl => $"https://www.youtube.com/embed/{this.VideoId}";

        public string ThumbnailUrl => $"https://i.ytimg.com/vi/{this.VideoId}/mqdefault.jpg";

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<WebEminar, IndexPageWebEminarViewModel>()
                   .ForMember(x => x.AverageVote, opt =>
                    opt.MapFrom(x => x.Votes.Count == 0 ? 0 : x.Votes.Average(v => v.Value)))
                   .ForMember(dest => dest.CommentsCount, opt => opt.MapFrom(src => src.Comments.Count()))
                   .ForMember(dest => dest.LikesCount, opt => opt.MapFrom(src => src.Likes.Count()));
        }
    }
}