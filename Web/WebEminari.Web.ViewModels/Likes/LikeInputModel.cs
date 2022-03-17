using System;
using System.Collections.Generic;
using System.Text;

using WebEminari.Services.Mapping;

namespace WebEminari.Web.ViewModels.Likes
{
    public class LikeInputModel : IMapTo<WebEminari.Data.Models.Like>
    {
        public int WebEminarId { get; set; }
    }
}

