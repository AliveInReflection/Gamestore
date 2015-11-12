using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameStore.WebUI.App_LocalResources.Localization;

namespace GameStore.WebUI.Models
{    
    public class DisplayGameViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int GameId { get; set; }

        [Display(ResourceType = typeof(ModelRes), Name = "GameGameKey")]
        public string GameKey { get; set; }

        [Display(ResourceType = typeof(ModelRes), Name = "GameGameName")]
        public string GameName { get; set; }

        [Display(ResourceType = typeof(ModelRes), Name = "GameDescription")]
        public string Description { get; set; }

        [Display(ResourceType = typeof(ModelRes), Name = "GamePrice")]
        public decimal Price { get; set; }

        [Display(ResourceType = typeof(ModelRes), Name = "GameUnitsInStock")]
        public short UnitsInStock { get; set; }

        [Display(ResourceType = typeof(ModelRes), Name = "GameDiscontinued")]
        public bool Discontinued { get; set; }

        [Display(ResourceType = typeof(ModelRes), Name = "GamePublicationDate")]
        public DateTime PublicationDate { get; set; }

        [Display(ResourceType = typeof(ModelRes), Name = "GameReceiptDate")]
        public DateTime ReceiptDate { get; set; }

        [Display(ResourceType = typeof(ModelRes), Name = "GamePublisher")]
        public DisplayPublisherViewModel Publisher { get; set; }

        [Display(ResourceType = typeof(ModelRes), Name = "GameGenres")]
        public IEnumerable<DisplayGenreViewModel> Genres { get; set; }

        [Display(ResourceType = typeof(ModelRes), Name = "GamePlatformTypes")]
        public IEnumerable<DisplayPlatformTypeViewModel> PlatformTypes { get; set; }

    }
}