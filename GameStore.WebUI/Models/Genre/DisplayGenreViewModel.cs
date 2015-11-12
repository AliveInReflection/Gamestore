using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using GameStore.WebUI.App_LocalResources.Localization;

namespace GameStore.WebUI.Models
{
    public class DisplayGenreViewModel
    {
        [Required]
        public int GenreId { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        [Display(ResourceType = typeof(ModelRes), Name = "GenreGenreName")]
        public string GenreName { get; set; }
    }
}
