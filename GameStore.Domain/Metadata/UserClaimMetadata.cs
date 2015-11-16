using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GameStore.Infrastructure.Enums;

namespace GameStore.Domain.Metadata
{
    public partial class UserClaimMetadata
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int ClaimId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public ClaimTypes ClaimType { get; set; }

        [Required]
        public Permissions ClaimValue { get; set; }
    }
}

       