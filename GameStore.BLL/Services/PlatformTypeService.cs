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
    }
}
