using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

using WebEminari.Data.Common.Models;

namespace WebEminari.Data.Models
{
    public class Organization : BaseDeletableModel<int>
    {
        public Organization()
        {
            this.Users = new HashSet<ApplicationUser>();
        }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public ICollection<ApplicationUser> Users { get; set; }

        public string CreatorId { get; set; }

        public ApplicationUser Creator { get; set; }
    }
}