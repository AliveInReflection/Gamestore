using System.Collections.Generic;

namespace GameStore.WebUI.Models
{
    public class FilteredGamesViewModel
    {
        public IEnumerable<DisplayGameViewModel> Games { get; set; }
        public ContentTransformationViewModel Transformer { get; set; }
    }
}