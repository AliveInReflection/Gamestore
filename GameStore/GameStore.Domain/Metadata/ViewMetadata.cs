using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GameStore.Domain.Entities;

namespace GameStore.Domain.Metadata
{
    public partial class ViewMetadata
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int ViewId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int GameId { get; set; }


        [ForeignKey("GameId")]
        public Game Game { get; set; }
    }
}
