using System;
using System.Collections.Generic;
using System.Text;

namespace WebEminari.Data.Models
{
    public class ChatApplicationUser
    {
          public int ChatId { get; set; }

        public Chat Chat { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
