using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.Domain.Metadata;
using GameStore.Infrastructure.Enums;

namespace GameStore.Domain.Entities
{
    [MetadataType(typeof(UserMetadata))]
    public partial class UserClaim
    {
        public int ClaimId { get; set; }
        public int UserId { get; set; }
        public ClaimTypes ClaimType { get; set; }
        public Permissions ClaimValue { get; set; }

        public virtual User User { get; set; }
    }
}
