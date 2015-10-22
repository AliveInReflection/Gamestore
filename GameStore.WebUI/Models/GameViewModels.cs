using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameStore.WebUI.Models
{
    public class CreateGameViewModel
    {
        [Required]
        [MinLength(3)]
        [MaxLength(10)]
        [Display(Name = "Key")]
        public string GameKey { get; set; }

        [Required]
        [MinLength(3)]
        [Display(Name = "Game name")]
        public string GameName { get; set; }

        [Required]
        [MinLength(3)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name="Price")]
        public decimal Price { get; set; }

        [Display(Name="Units in stock")]
        public Int16 UnitsInStock { get; set; }

        [Display(Name="Is discontinued")]
        public Boolean Discontinued { get; set; }

        [Required]
        [Display(Name = "Pablisher")]
        public int PublisherId { get; set; }

        [Required]
        [Display(Name = "Genres")]
        public IEnumerable<int> GenreIds { get; set; }

        [Required]
        [Display(Name = "Platforms")]
        public IEnumerable<int> PlatformTypeIds { get; set; }
        
    }


    public class EditGameViewModel
    {
        [Required]
        [HiddenInput(DisplayValue = false)]
        public int GameId { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(10)]
        [Display(Name = "Key")]
        public string GameKey { get; set; }

        [Required]
        [MinLength(3)]
        [Display(Name = "Game name")]
        public string GameName { get; set; }

        [Required]
        [MinLength(3)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Price")]
        public decimal Price { get; set; }

        [Display(Name = "Units in stock")]
        public Int16 UnitsInStock { get; set; }

        [Display(Name = "Is discontinued")]
        public Boolean Discontinued { get; set; }

        [Required]
        [ScaffoldColumn(false)]
        [Display(Name = "Publisher")]
        public int PublisherId { get; set; }

        [Required]
        [ScaffoldColumn(false)]
        [Display(Name = "Genres")]
        public IEnumerable<int> GenreIds { get; set; }

        [Required]
        [ScaffoldColumn(false)]
        [Display(Name = "Platforms")]
        public IEnumerable<int> PlatformTypeIds { get; set; }

    }

    public class DisplayGameViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int GameId { get; set; }

        [Display(Name="Key")]
        public string GameKey { get; set; }

        [Display(Name = "Game name")]
        public string GameName { get; set; }

        [Display(Name = "Descriptions")]
        public string Description { get; set; }

        [Display(Name = "Price")]
        public decimal Price { get; set; }

        [Display(Name = "Units in stock")]
        public Int16 UnitsInStock { get; set; }

        [Display(Name = "Is discontinued")]
        public Boolean Discontinued { get; set; }

        [Display(Name="Publisher")]
        public DisplayPublisherViewModel Publisher { get; set; }

        [Display(Name = "Genres")]
        public IEnumerable<GenreViewModel> Genres { get; set; }

        [Display(Name = "Platforms")]
        public IEnumerable<PlatformTypeViewModel> PlatformTypes { get; set; }

    }
}