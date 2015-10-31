using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace GameStore.WebUI.Models
{
    public class FilteringViewModel
    {
        [Display(Name="Filter by genres")]
        public List<CheckBoxViewModel> Genres { get; set; }
        
        [Display(Name = "Filter by platform types")]
        public List<CheckBoxViewModel> PlatformTypes { get; set; }
        
        [Display(Name = "Filter by publishers")]
        public List<CheckBoxViewModel> Publishers { get; set; }

        [Display(Name = "Sort by")]
        public List<SelectListItem> SortByItems { get; set; }
        public string SortBy { get; set; }

        [Display(Name = "Filter by publishing date")]
        public List<RadiobuttonViewModel> PublishingDates { get; set; }
        public RadiobuttonViewModel PublishingDate { get; set; }

        [Display(Name="Minimal price")]
        public decimal MinPrice { get; set; }
        [Display(Name = "Maximal price")]
        public decimal MaxPrice { get; set; }

        [Display(Name = "Part of name")]
        [MinLength(3)]
        public string Name { get; set; }

        [Display(Name = "Items per page")]
        public List<SelectListItem> ItemsPerPageList { get; set; }
        
        public string ItemsPerPage { get; set; }

        public int PageCount { get; set; }

        public int CurrentPage { get; set; }
    }
}