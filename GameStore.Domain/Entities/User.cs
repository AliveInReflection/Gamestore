using GameStore.Domain.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.Entities
{
    [MetadataType(typeof(UserMetadata))]
    public class User : BaseEntity
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime? BanExpirationDate { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
        public virtual ICollection<UserClaim> Claims { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
