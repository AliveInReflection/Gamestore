using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameStore.WebUI.Models
{
    public class UpdateGameViewModel
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
        [Range(0.01d, double.MaxValue)]
        public decimal Price { get; set; }

        [Display(Name = "Units in stock")]
        [Range(0, int.MaxValue)]
        public short UnitsInStock { get; set; }

        [Display(Name = "Is discontinued")]
        public bool Discontinued { get; set; }

        [Display(Name = "Publication date")]
        public DateTime PublicationDate { get; set; }

        [Display(Name = "Receipt date")]
        public DateTime ReceiptDate { get; set; }

        [Required]
        [Display(Name = "Publisher")]
        public int PublisherId { get; set; }

        [Required]
        [Display(Name = "Genres")]
        public IEnumerable<int> GenreIds { get; set; }

        [Required]
        [Display(Name = "Platforms")]
        public IEnumerable<int> PlatformTypeIds { get; set; }

    }
}