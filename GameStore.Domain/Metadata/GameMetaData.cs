using GameStore.Domain.Entities.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.Entities
{
    public partial class GameMetadata
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int GameId { get; set; }

        [Required]
        [MaxLength(100)]
        public string GameKey { get; set; }

        [Required]
        [MaxLength(100)]
        public string GameName { get; set; }

        [Required]
        [MaxLength(400)]
        public string Description { get; set; }

    }
}
