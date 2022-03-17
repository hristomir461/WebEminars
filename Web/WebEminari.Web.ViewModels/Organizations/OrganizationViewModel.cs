using System;
using System.Collections.Generic;
using System.Text;

using WebEminari.Services.Mapping;
using WebEminari.Web.ViewModels.ApplicationUser;

namespace WebEminari.Web.ViewModels.Organizations
{
    public class OrganizationViewModel : IMapFrom<Data.Models.Organization>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedOn { get; set; }

        public int UsersCount { get; set; }

        public string CreatorId { get; set; }

        public string CreatorUserName { get; set; }

        public ICollection<UserViewModel> Users { get; set; }
    }
}
