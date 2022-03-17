// ReSharper disable VirtualMemberCallInConstructor
namespace WebEminari.Data.Models
{
    using System;
    using System.Collections.Generic;

    using WebEminari.Data.Common.Models;

    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
            this.Votes = new HashSet<Vote>();
            this.UserBookings = new HashSet<UserBooking>();
            this.Comments = new HashSet<Comment>();
            this.Chats = new HashSet<ChatApplicationUser>();
            this.Likes = new HashSet<Like>();
            this.Reports = new HashSet<Report>();
            this.WebEminars = new HashSet<WebEminar>();
            this.Organizations = new HashSet<Organization>();
            this.CreatedOrganizations = new HashSet<Organization>();
        }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ImagePath { get; set; }
        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }

        public virtual ICollection<Vote> Votes { get; set; }

        public virtual ICollection<UserBooking> UserBookings { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<ChatApplicationUser> Chats { get; set; }

        public ICollection<Like> Likes { get; set; }

        public ICollection<Report> Reports { get; set; }

        public ICollection<WebEminar> WebEminars { get; set; }

        public IEnumerable<Organization> CreatedOrganizations { get; set; }

        public IEnumerable<Organization> Organizations { get; set; }
    }
}
