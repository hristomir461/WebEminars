using System;
using System.Collections.Generic;
using System.Text;

using AutoMapper;

using WebEminari.Data.Models;
using WebEminari.Services.Mapping;

namespace WebEminari.Web.ViewModels.WebEminars
{
    public class WebEminarsListViewModel : PagingViewModel, IMapFrom<WebEminar>
    {
        public IEnumerable<WebEminarsInListViewModel> WebEminars { get; set; }

        public IEnumerable<WebEminarViewModel> Vebinari { get; set; }

        public string SearchText { get; set; }

        public int CategoryId { get; set; }

        public string StateFilter { get; set; }

        public IEnumerable<KeyValuePair<string, string>> CategoriesItems { get; set; }

    }
}
