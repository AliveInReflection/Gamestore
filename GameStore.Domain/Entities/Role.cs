using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.Domain.Metadata;

namespace GameStore.Domain.Entities
{
    [MetadataType(typeof(RoleMetadata))]
    public partial class Role : BaseEntity
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }

        public virtual ICollection<RoleClaim> RoleClaims { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
