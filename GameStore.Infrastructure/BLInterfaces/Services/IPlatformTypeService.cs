using System.Collections.Generic;
using GameStore.Infrastructure.DTO;


namespace GameStore.Infrastructure.BLInterfaces
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
