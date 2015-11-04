using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Infrastructure;
using GameStore.BLL.Interfaces;
using GameStore.DAL.Interfaces;
using GameStore.Domain.Entities;

namespace GameStore.BLL.Services
{
    public class PlatformTypeService : IPlatformTypeService
    {
        private readonly IUnitOfWork database;

        public PlatformTypeService()
        {
            
        }

        public PlatformTypeService(IUnitOfWork database)
        {
            this.database = database;
        }

        public IEnumerable<PlatformTypeDTO> GetAll()
        {
            var platformTypes = database.PlatformTypes.GetAll();
            return Mapper.Map<IEnumerable<PlatformType>, IEnumerable<PlatformTypeDTO>>(platformTypes);
        }

        public IEnumerable<PlatformTypeDTO> Get(string gameKey)
        {
            var platformTypes = database.PlatformTypes.GetMany(m => m.Games.Any(g => g.GameKey.Equals(gameKey)));
            return Mapper.Map<IEnumerable<PlatformType>, IEnumerable<PlatformTypeDTO>>(platformTypes);
        }

        public PlatformTypeDTO Get(int platformTypeId)
        {
            var entry = database.PlatformTypes.Get(m => m.PlatformTypeId.Equals(platformTypeId));
            return Mapper.Map<PlatformType, PlatformTypeDTO>(entry);
        }

        public void Create(PlatformTypeDTO platformType)
        {
            if (platformType == null)
            {
                throw new ValidationException("No content received");
            }

            try
            {
                var platformTypeToSave = Mapper.Map<PlatformTypeDTO, PlatformType>(platformType);
                database.PlatformTypes.Create(platformTypeToSave);
                database.Save();
            }
            catch (InvalidOperationException)
            {
                throw new ValidationException(String.Format("Another platform type with the same name ({0}) exists", platformType.PlatformTypeName));
            }

        }

        public void Update(PlatformTypeDTO platformType)
        {
            if (platformType == null)
            {
                throw new ValidationException("No content received");
            }

            try
            {
                var platformTypeToSave = Mapper.Map<PlatformTypeDTO, PlatformType>(platformType);
                database.PlatformTypes.Update(platformTypeToSave);
                database.Save();
            }
            catch (InvalidOperationException)
            {
                throw new ValidationException(String.Format("Another platform type with the same name ({0}) exists", platformType.PlatformTypeName));
            }
        }

        public void Delete(int platformTypeId)
        {
            database.PlatformTypes.Delete(platformTypeId);
            database.Save();
        }
    }
}
