using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.Infrastructure.Enums;

namespace GameStore.Infrastructure.DTO
{
    public class NotificationInfoDTO
    {
        public int UserId { get; set; }

        public NotificationMethod NotificationMethod { get; set; }

        public string Target { get; set; }

        public virtual UserDTO User { get; set; }
    }
}
