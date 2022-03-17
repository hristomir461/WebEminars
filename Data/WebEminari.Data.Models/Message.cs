using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

using WebEminari.Data.Common.Models;

namespace WebEminari.Data.Models
{
    public class Message : BaseModel<int>
    {
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string Text { get; set; }

        public int ChatId { get; set; }

        public Chat Chat { get; set; }
    }
}