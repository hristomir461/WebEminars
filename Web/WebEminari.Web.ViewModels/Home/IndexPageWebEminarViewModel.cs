using System;
using System.Collections.Generic;
using System.Text;

using WebEminari.Data.Models;
using WebEminari.Services.Mapping;

namespace WebEminari.Web.ViewModels.Home
{
    public class IndexPageWebEminarViewModel : IMapFrom<WebEminar>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string CategoryName { get; set; }

        public string ImageName { get; set; }
    }
}
