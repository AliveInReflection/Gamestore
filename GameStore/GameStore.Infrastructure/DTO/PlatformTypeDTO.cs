using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Infrastructure.DTO
{
    public class PlatformTypeDTO
    {
        public int PlatformTypeId { get; set; }
        public string PlatformTypeName { get; set; }

        public IEnumerable<GameDTO> Games { get; set; }
    }
}
