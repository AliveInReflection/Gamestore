using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using GameStore.WebUI.App_LocalResources.Localization;

namespace GameStore.WebUI.Models
{
    public class ContentTransformationViewModel
    {
        [Display(ResourceType = typeof(ModelRes), Name = "GameFilterGenres")]
        public IEnumerable<int> Genres { get; set; }
        public List<CheckBoxViewModel> GenreItems { get; set; }

        [Display(ResourceType = typeof(ModelRes), Name = "GameFilterPlatformTypes")]
        public IEnumerable<int> PlatformTypes { get; set; }
        public List<CheckBoxViewModel> PlatformTypeItems { get; set; }

        [Display(ResourceType = typeof(ModelRes), Name = "GameFilterPublishers")]
        public IEnumerable<int> Publishers { get; set; }
        public List<CheckBoxViewModel> PublisherItems { get; set; }

        [Display(ResourceType = typeof(ModelRes), Name = "GameFilterSortByItems")]
        public List<SelectListItem> SortByItems { get; set; }
        [Display(ResourceType = typeof(ModelRes), Name = "GameFilterSortByItems")]
        public string SortBy { get; set; }

        [Display(ResourceType = typeof(ModelRes), Name = "GameFilterPublicationDate")]
        public List<RadiobuttonViewModel> PublishingDates { get; set; }
        public RadiobuttonViewModel PublishingDate { get; set; }

        [Display(ResourceType = typeof(ModelRes), Name = "GameFilterMinPrice")]
        [Range(0, int.MaxValue, ErrorMessageResourceType = typeof(ValidationRes), ErrorMessageResourceName = "OnlyPositiveError")]
        public decimal MinPrice { get; set; }


        [Display(ResourceType = typeof(ModelRes), Name = "GameFilterMaxPrice")]
        [Range(0, int.MaxValue, ErrorMessageResourceType = typeof(ValidationRes), ErrorMessageResourceName = "OnlyPositiveError")]
        public decimal MaxPrice { get; set; }

        [Display(ResourceType = typeof(ModelRes), Name = "GameFilterName")]
        [MinLength(3)]
        public string Name { get; set; }

        
        public List<SelectListItem> ItemsPerPageList { get; set; }
        
        [Display(ResourceType = typeof(ModelRes), Name = "GameFilterItemsPerPage")]
        public string ItemsPerPage { get; set; }

        public int PageCount { get; set; }

        public int CurrentPage { get; set; }
    }
}