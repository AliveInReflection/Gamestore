using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameStore.Domain.Entities
{
    public partial class GameMetadata
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int GameId { get; set; }

        [Required]
        [MaxLength(100)]
        public string GameKey { get; set; }

        [Required]
        [MaxLength(200)]
        public string GameName { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public short UnitsInStock { get; set; }

        [Required]
        public Boolean Discontinued { get; set; }

        [Required]
        public DateTime PublicationDate { get; set; }

        [Required]
        public DateTime ReceiptDate { get; set; }

        [Required]
        public int PublisherId { get; set; }



        [NotMapped]
        public ICollection<Comment> Comments { get; set; }
        [NotMapped]
        public ICollection<Genre> Genres { get; set; }
        [NotMapped]
        public Publisher Publisher { get; set; }
        [NotMapped]
        public ICollection<View> Views { get; set; }

    }
}
