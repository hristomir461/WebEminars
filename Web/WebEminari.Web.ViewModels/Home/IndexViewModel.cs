using System;
using System.Collections.Generic;
using System.Text;

using WebEminari.Data.Models;
using WebEminari.Web.ViewModels.WebEminars;

namespace WebEminari.Web.ViewModels.Home
{
    public class IndexViewModel
    {
        public IEnumerable<IndexPageWebEminarViewModel> RandomWebEminars { get; set; }

        public IEnumerable<WebEminarViewModel> Webinars { get; set; }
        public IEnumerable<WebEminarViewModel> KacheniWebinars { get; set; }
    }
}
