using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameStore.Domain.Entities
{
    public partial class CommentMetadata
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int CommentId { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        [MaxLength(400)]
        public string Content { get; set; }

        [NotMapped]
        public Game Game { get; set; }
    }
}
