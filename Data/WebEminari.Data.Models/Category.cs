using System;
using System.Collections.Generic;
using System.Text;

using WebEminari.Data.Common.Models;

namespace WebEminari.Data.Models
{
    public class Category : BaseDeletableModel<int>
    {
        public Category()
        {
            this.WebEminars = new HashSet<WebEminar>();
        }

        public string Name { get; set; }

        public ICollection<WebEminar> WebEminars { get; set; }
    }
}
