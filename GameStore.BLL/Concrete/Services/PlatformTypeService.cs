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
            var game = database.Games.GetSingle(m => m.GameKey.Equals(gameKey));

            var platformTypes = game.PlatformTypes;
            return Mapper.Map<IEnumerable<PlatformType>, IEnumerable<PlatformTypeDTO>>(platformTypes);
        }

        public PlatformTypeDTO Get(int platformTypeId)
        {
            var entry = database.PlatformTypes.GetSingle(m => m.PlatformTypeId.Equals(platformTypeId));
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
                var entry = database.PlatformTypes.GetSingle(m => m.PlatformTypeName.Equals(platformType.PlatformTypeName));
                throw new ValidationException("Another platform type with the same name exists");
            }
            catch (InvalidOperationException)
            {
                var platformTypeToSave = Mapper.Map<PlatformTypeDTO, PlatformType>(platformType);
                database.PlatformTypes.Create(platformTypeToSave);
                database.Save();
            }

        }

        public void Update(PlatformTypeDTO platformType)
        {
            if (platformType == null)
            {
                throw new ValidationException("No content received");
            }

            var entry = database.PlatformTypes.GetSingle(m => m.PlatformTypeName.Equals(platformType.PlatformTypeName));
            if (entry.PlatformTypeId != platformType.PlatformTypeId)
            {
                throw new ValidationException("Another platform type with the same name exists");
            }

            var platformTypeToSave = Mapper.Map(platformType, entry);
            database.PlatformTypes.Update(platformTypeToSave);
            database.Save();
        }

        public void Delete(int platformTypeId)
        {
            var entry = database.PlatformTypes.GetSingle(m => m.PlatformTypeId.Equals(platformTypeId));

            if(entry.Games.Any())
            {
                throw new ValidationException("There are some games that marked up by this platform type in the store");
            }

            database.PlatformTypes.Delete(platformTypeId);
            database.Save();
        }
    }
}
