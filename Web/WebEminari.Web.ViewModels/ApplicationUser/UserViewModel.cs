using System;
using System.Collections.Generic;
using System.Text;

using WebEminari.Services.Mapping;

namespace WebEminari.Web.ViewModels.ApplicationUser
{
    public class UserViewModel : IMapFrom<Data.Models.ApplicationUser>
    {
        public string Id { get; set; }

        public string UserName { get; set; }
    }
}
