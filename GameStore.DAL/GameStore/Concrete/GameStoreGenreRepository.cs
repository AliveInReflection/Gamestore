using System.Linq;
using AutoMapper;
using Gamestore.DAL.Context;
using GameStore.DAL.Infrastructure;
using GameStore.Domain.Entities;

namespace GameStore.DAL.GameStore.Concrete
{
    public class GameStoreGenreRepository : BaseGameStoreRepository<Genre>
    {
        public GameStoreGenreRepository(GameStoreContext context)
            :base(context)
        {
            
        }

        public override void Create(Genre entity)
        {
            if (entity.GenreId == 0)
            {
                entity.GenreId = GetId();
            }
            base.Create(entity);
        }

        public override void Delete(int id)
        {
            var entry = context.Genres.First(m => m.GenreId.Equals(id));
            entry.IsDeleted = true;
        }

        public override void Update(Genre entity)
        {
            var entry = context.Genres.First(m => m.GenreId.Equals(entity.GenreId));
            Mapper.Map(entity, entry);
        }

        private int GetId()
        {
            return context.Genres.Select(m => m.GenreId).OrderBy(m => m).ToList().Last() + KeyManager.Coefficient;
        }
    }
}
