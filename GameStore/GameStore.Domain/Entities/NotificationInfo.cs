using System.ComponentModel.DataAnnotations;
using GameStore.Domain.Metadata;
using GameStore.Infrastructure.Enums;

namespace GameStore.Domain.Entities
{
    [MetadataType(typeof(NotificationInfoMetadata))]
    public partial class NotificationInfo : BaseEntity
    {
        public int UserId { get; set; }

        public NotificationMethod NotificationMethod { get; set; }

        public string Target { get; set; }

        public virtual User User { get; set; }
    }
}
