using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.Entities
{
    public class PlatformType
    {
        public int PlatformTypeId { get; set; }
        public string PlatformTypeName { get; set; }

        public IEnumerable<Game> Games { get; set; }
    }
}
