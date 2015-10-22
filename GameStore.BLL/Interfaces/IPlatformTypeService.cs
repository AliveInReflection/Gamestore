using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.BLL.DTO;

namespace GameStore.BLL.Interfaces
{
    public interface IPlatformTypeService
    {
        IEnumerable<PlatformTypeDTO> GetAll();
        IEnumerable<PlatformTypeDTO> Get(string gameKey);
    }
}
