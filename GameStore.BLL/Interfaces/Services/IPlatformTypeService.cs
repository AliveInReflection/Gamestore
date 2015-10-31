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
        void Create(PlatformTypeDTO platformType);
        void Update(PlatformTypeDTO platformType);
        void Delete(int platformTypeId);

        IEnumerable<PlatformTypeDTO> GetAll();
        IEnumerable<PlatformTypeDTO> Get(string gameKey);
        PlatformTypeDTO Get(int platformTypeId);
    }
}
