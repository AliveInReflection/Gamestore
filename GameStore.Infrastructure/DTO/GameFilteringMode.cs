using System;
using System.Collections.Generic;
using GameStore.Infrastructure.Enums;

namespace GameStore.Infrastructure.DTO
{
    public class GameFilteringMode
    {
        public IEnumerable<int> GenreIds { get; set; }
        public IEnumerable<int> PlatformTypeIds { get; set; }
        public IEnumerable<int> PublisherIds { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public TimeSpan PublishingDate { get; set; }
        public string PartOfName { get; set; }
    }    
}
