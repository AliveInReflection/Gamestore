using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameStore.WebUI.App_LocalResources.Localization;

namespace GameStore.WebUI.Models
{
    public class UpdateGameViewModel
    {
        [Required]
        [HiddenInput(DisplayValue = false)]
        [Display(ResourceType = typeof(ModelRes), Name = "GameGameId")]
        public int GameId { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(40)]
        [Display(ResourceType = typeof(ModelRes), Name = "GameGameKey")]
        public string GameKey { get; set; }

        [Required]
        [MinLength(3)]
        [Display(ResourceType = typeof(ModelRes), Name = "GameGameName")]
        public string GameName { get; set; }

        [Required]
        [MinLength(3)]
        [Display(ResourceType = typeof(ModelRes), Name = "GameDescription")]
        public string Description { get; set; }

        [Display(ResourceType = typeof(ModelRes), Name = "GamePrice")]
        [Range(0.01d, double.MaxValue)]
        public decimal Price { get; set; }

        [Display(ResourceType = typeof(ModelRes), Name = "GameUnitsInStock")]
        [Range(0, int.MaxValue)]
        public short UnitsInStock { get; set; }

        [Display(ResourceType = typeof(ModelRes), Name = "GameDiscontinued")]
        public bool Discontinued { get; set; }

        [Display(ResourceType = typeof(ModelRes), Name = "GamePublicationDate")]
        public DateTime PublicationDate { get; set; }

        [Display(ResourceType = typeof(ModelRes), Name = "GameReceiptDate")]
        public DateTime ReceiptDate { get; set; }

        [Required]
        [Display(ResourceType = typeof(ModelRes), Name = "GamePublisher")]
        public int PublisherId { get; set; }
        public IEnumerable<SelectListItem> Publishers { get; set; }

        [Required]
        [Display(ResourceType = typeof(ModelRes), Name = "GameGenres")]
        public IEnumerable<int> GenreIds { get; set; }
        public IEnumerable<SelectListItem> Genres { get; set; }

        [Required]
        [Display(ResourceType = typeof(ModelRes), Name = "GamePlatformTypes")]
        public IEnumerable<int> PlatformTypeIds { get; set; }
        public IEnumerable<SelectListItem> PlatformTypes { get; set; }

    }
}