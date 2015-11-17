using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Gamestore.DAL.Context;
using GameStore.Domain.Entities;

namespace GameStore.DAL.GameStore.Concrete
{
    public class GameStorePlatformTypeRepository : BaseGameStoreRepository<PlatformType>
    {
        public GameStorePlatformTypeRepository(GameStoreContext context)
            : base(context)
        {

        }

        public override void Delete(int id)
        {
            var entry = context.PlatformTypes.First(m => m.PlatformTypeId.Equals(id));
            entry.IsDeleted = true;
        }

        public override void Update(PlatformType entity)
        {
            var entry = context.PlatformTypes.First(m => m.PlatformTypeId.Equals(entity.PlatformTypeId));
            Mapper.Map(entity, entry);
        }
    }
}
