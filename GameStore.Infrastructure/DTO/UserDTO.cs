using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Infrastructure.DTO
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string UserName { get; set; }

        public IEnumerable<CommentDTO> Comments { get; set; }
    }
}
