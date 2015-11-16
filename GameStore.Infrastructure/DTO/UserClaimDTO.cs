using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameStore.Infrastructure.Enums;

namespace GameStore.Infrastructure.DTO
{
    public class UserClaimDTO
    {
        public int ClaimId { get; set; }
        public int UserId { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }

        public virtual UserDTO User { get; set; }
    }
}
