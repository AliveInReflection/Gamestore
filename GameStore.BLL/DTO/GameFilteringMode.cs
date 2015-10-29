using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.BLL.DTO
{
    public class GameFilteringMode
    {
        public IEnumerable<int> GenreIds { get; set; }
        public IEnumerable<int> PlatformTypeIds { get; set; }
        public IEnumerable<int> PublisherIds { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public GamesSortingMode SortingMode { get; set; }
        public TimeSpan PublishingDate { get; set; }
        public string PartOfName { get; set; }
        public int ItemsPerPage { get; set; }
    }

    public enum GamesSortingMode
    {
        None = 0,
        MostPopular = 1,
        MostCommented = 2,
        PriceAscending = 3,
        PriceDescending = 4,
        AdditionDate = 5
    }
}
