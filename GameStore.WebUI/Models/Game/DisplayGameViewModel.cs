using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameStore.WebUI.Models
{    
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
        public short UnitsInStock { get; set; }

        [Display(Name = "Is discontinued")]
        public bool Discontinued { get; set; }

        [Display(Name = "Publication date")]
        public DateTime PublicationDate { get; set; }

        [Display(Name = "Receipt date")]
        public DateTime ReceiptDate { get; set; }

        [Display(Name="Publisher")]
        public DisplayPublisherViewModel Publisher { get; set; }

        [Display(Name = "Genres")]
        public IEnumerable<DisplayGenreViewModel> Genres { get; set; }

        [Display(Name = "Platforms")]
        public IEnumerable<DisplayPlatformTypeViewModel> PlatformTypes { get; set; }

    }
}