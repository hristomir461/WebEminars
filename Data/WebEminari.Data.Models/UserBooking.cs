using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using WebEminari.Data.Common.Models;

namespace WebEminari.Data.Models
{


    public class UserBooking : BaseDeletableModel<int>
    {
        public UserBooking()
        { }
        public UserBooking(string userId, int webEminarId, string userFirstName, string userLastName, string userEmail)
        {
            UserId = userId;
            UserFirstName = userFirstName;
            UserLastName = userLastName;
            UserEmail = userEmail;
            WebEminarId = webEminarId;
        }
        public string UserId { get; set; }

        public string UserFirstName { get; set; }

        public string UserEmail { get; set; }

        public string UserLastName { get; set; }

        public int WebEminarId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual WebEminar WebEminar { get; set; }
    }
}

