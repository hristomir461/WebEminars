using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

using WebEminari.Services.Mapping;

namespace WebEminari.Web.ViewModels.Organizations
{
    public class OrganizationEditModel : IMapFrom<Data.Models.Organization>
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Display(Name = "Is deleted")]
        public bool IsDeleted { get; set; }

        public string CreatorId { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
