using System;
using System.Collections.Generic;
using System.Text;

namespace WebEminari.Web.ViewModels.WebEminars
{
    public class WebEminarsListViewModel : PagingViewModel
    {
        public IEnumerable<WebEminarsInListViewModel> WebEminars { get; set; }

    }
}
