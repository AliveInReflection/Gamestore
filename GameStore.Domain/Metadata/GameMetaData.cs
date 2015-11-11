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


        [ForeignKey("PublisherId")]
        public virtual Publisher Publisher { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Genre> Genres { get; set; }
        public virtual ICollection<View> Views { get; set; }

    }
}
