using GameStore.Domain.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameStore.Domain.Metadata
{
    public partial class CommentMetadata
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int CommentId { get; set; }

        [Required]
        [MaxLength(100)]
        public string SendersName { get; set; }

        [Required]
        [MaxLength(400)]
        public string Content { get; set; }

        [Required]
        public int GameId { get; set; }

    }
}
