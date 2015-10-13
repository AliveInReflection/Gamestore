using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameStore.Domain.Entities
{
    public partial class GenreMetadata
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int GenreId { get; set; }

        [Required]
        [MaxLength(100)]
        public string GenreName { get; set; }

        public int? ParentGenreId { get; set; }

    }
}
