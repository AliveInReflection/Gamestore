using System.Data;
using System.Linq;
using AutoMapper;
using Gamestore.DAL.Context;
using GameStore.DAL.Infrastructure;
using GameStore.Domain.Entities;

namespace GameStore.DAL.GameStore.Concrete
{
    public class GameStoreGameRepository : BaseGameStoreRepository<Game>
    {
        public GameStoreGameRepository(GameStoreContext context)
            :base(context)
        {
            
        }

        public override void Create(Game entity)
        {
            if (entity.GameId == 0)
            {
                entity.GameId = GetId();
            }
            base.Create(entity);
        }

        public override void Delete(int id)
        {
            var entry = context.Games.First(m => m.GameId.Equals(id));
            entry.IsDeleted = true;
        }

        public override void Update(Game entity)
        {
            var entry = context.Games.First(m => m.GameId.Equals(entity.GameId));
            entry.GameKey = entity.GameKey;
            entry.GameName = entity.GameName;
            entry.Description = entity.Description;
            entry.PublicationDate = entity.PublicationDate;
            entry.ReceiptDate = entity.ReceiptDate;
            entry.Price = entity.Price;
            entry.Discontinued = entity.Discontinued;
            entry.UnitsInStock = entity.UnitsInStock;
            entry.Publisher = entity.Publisher;
            
            entry.Genres.Clear();
            foreach (var genre in entity.Genres)
            {
                entry.Genres.Add(genre);
            }
            entry.PlatformTypes.Clear();
            foreach (var platformType in entity.PlatformTypes)
            {
                entry.PlatformTypes.Add(platformType);
            }

        }

        private int GetId()
        {
            return context.Games.Select(m => m.GameId).OrderBy(m => m).ToList().Last() + KeyManager.Coefficient;
        }
    }
}
