using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.Domain.Entities;
using GameStore.Infrastructure.Enums;

namespace GameStore.Domain.Metadata
{
    public partial class NotificationInfoMetadata
    {
        [Key, ForeignKey("User")]
        public int UserId { get; set; }

        [Required]
        public NotificationMethod NotificationMethod { get; set; }

        [Required]
        public string Target { get; set; }

        [Required]
        public virtual User User { get; set; }
    }
}
