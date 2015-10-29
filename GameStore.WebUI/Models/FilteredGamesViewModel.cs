using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameStore.WebUI.Models
{
    public class FilteredGamesViewModel
    {
        public IEnumerable<DisplayGameViewModel> Games { get; set; }
        public FilteringViewModel Filter { get; set; }
    }
}