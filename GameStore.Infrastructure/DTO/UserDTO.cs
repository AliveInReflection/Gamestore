using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace GameStore.Infrastructure.DTO
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime? BanExpirationDate { get; set; }

        public IEnumerable<RoleDTO> Roles { get; set; }
        public IEnumerable<Claim> Claims { get; set; }
        public IEnumerable<CommentDTO> Comments { get; set; }
    }
}
