using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

using WebEminari.Data.Common.Models;

namespace WebEminari.Data.Models
{
    public class Chat : BaseModel<int>
    {
        public Chat()
        {
            this.Messages = new HashSet<Message>();
            this.ChatApplicationUsers = new HashSet<ChatApplicationUser>();
        }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public ICollection<Message> Messages { get; set; }

        public ICollection<ChatApplicationUser> ChatApplicationUsers { get; set; }
    }
}
