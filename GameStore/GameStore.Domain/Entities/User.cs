using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GameStore.Domain.Metadata;
using GameStore.Infrastructure.Enums;

namespace GameStore.Domain.Entities
{
    [MetadataType(typeof(UserMetadata))]
    public partial class User : BaseEntity
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Country { get; set; }
        public DateTime? BanExpirationDate { get; set; }

        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public NotificationMethod NotificationMethod { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
        public virtual ICollection<UserClaim> Claims { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

        
    }
}
