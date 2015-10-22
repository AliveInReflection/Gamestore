using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.Metadata
{
    public partial class PublisherMetadata
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int PublisherId { get; set; }

        [Required]
        [MaxLength(40)]
        public string CompanyName { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string HomePage { get; set; }

        
    }
}
