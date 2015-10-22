using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace GameStore.WebUI.Models
{
    public class PlatformTypeViewModel
    {
        [Required]
        public int PlatformTypeId { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        [Display(Name="Platform type")]
        public string PlatformTypeName { get; set; }
    }
}
