using System;
using System.Collections.Generic;
using System.Text;

using WebEminari.Data.Common.Models;

namespace WebEminari.Data.Models
{
    public class OrganizationUser
    {
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public int OrganizationId { get; set; }

        public Organization Organization { get; set; }
    }
}