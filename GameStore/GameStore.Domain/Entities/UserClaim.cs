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
    [MetadataType(typeof(UserClaimMetadata))]
    public partial class UserClaim
    {
        public int ClaimId { get; set; }
        public int UserId { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }

        public virtual User User { get; set; }
    }
}
