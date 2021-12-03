using System;
using System.Collections.Generic;
using System.Text;

using WebEminari.Data.Models;
using WebEminari.Services.Mapping;

namespace WebEminari.Web.ViewModels.WebEminars
{
    public class WebEminarsInListViewModel : IMapFrom<WebEminar>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string MeetLink { get; set; }

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
