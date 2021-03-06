﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [MaxLength(200)]
        public string GameName { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }

    }
}
