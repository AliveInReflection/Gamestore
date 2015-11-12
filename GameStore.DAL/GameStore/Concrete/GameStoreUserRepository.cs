using GameStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Gamestore.DAL.Context;

namespace GameStore.DAL.GameStore.Concrete
{
    public class GameStoreUserRepository : BaseGameStoreRepository<User>
    {
        public GameStoreUserRepository(GameStoreContext context)
            :base(context)
        {
            
        }

        public override void Delete(int id)
        {
            var entry = context.Users.First(m => m.UserId.Equals(id));
            entry.IsDeleted = true;
        }

        public override void Update(User entity)
        {
            var entry = context.Users.First(m => m.UserId.Equals(entity.UserId));
            Mapper.Map(entity, entry);
        }
    }
}
