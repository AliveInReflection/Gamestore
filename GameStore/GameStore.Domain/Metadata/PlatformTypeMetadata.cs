using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameStore.Domain.Entities
{
    public partial class PlatformTypeMetadata
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int PlatformTypeId { get; set; }

        [Required]
        [MaxLength(100)]
        public string PlatformTypeName { get; set; }

    }
}
