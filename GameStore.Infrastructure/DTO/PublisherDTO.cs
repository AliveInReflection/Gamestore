using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Infrastructure.DTO
{
    public class PublisherDTO
    {
        public int PublisherId { get; set; }
        public string CompanyName { get; set; }
        public string Description { get; set; }
        public string HomePage { get; set; }

        public IEnumerable<GameDTO> Games { get; set; }
    }
}
