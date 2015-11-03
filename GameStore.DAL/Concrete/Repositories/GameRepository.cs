using GameStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GameStore.DAL.Interfaces;
using Gamestore.DAL.Context;

namespace GameStore.DAL.Concrete.Repositories
{
    public class GameRepository : IRepository<Game>
    {
        private GameStoreContext context;

        public GameRepository(GameStoreContext context)
        {
            this.context = context;
        }

        public void Create(Game entity)
        {
            var genreIds = entity.Genres.Select(m => m.GenreId);
            var genres = context.Genres.Where(m => genreIds.Contains(m.GenreId));

            var platformTypeIds = entity.PlatformTypes.Select(m => m.PlatformTypeId);
            var platformTypes = context.PlatformTypes.Where(m => platformTypeIds.Contains(m.PlatformTypeId));

            var publisher = context.Publishers.First(m => m.PublisherId.Equals(entity.Publisher.PublisherId));

            entity.Genres = genres.ToList();
            entity.PlatformTypes = platformTypes.ToList();
            entity.Publisher = publisher;

            context.Games.Add(entity);
        }

        public void Update(Game entity)
        {
            var genreIds = entity.Genres.Select(m => m.GenreId);
            var genres = context.Genres.Where(m => genreIds.Contains(m.GenreId)).ToList();

            var platformTypeIds = entity.PlatformTypes.Select(m => m.PlatformTypeId);
            var platformTypes = context.PlatformTypes.Where(m => platformTypeIds.Contains(m.PlatformTypeId)).ToList();

            var publisher = context.Publishers.First(m => m.PublisherId.Equals(entity.Publisher.PublisherId));

            var entry = context.Games.First(m => m.GameId.Equals(entity.GameId));

            Mapper.CreateMap<Game, Game>().ForMember(m => m.Genres, opt => opt.Ignore())
                .ForMember(m => m.PlatformTypes, opt => opt.Ignore()).ForMember(m => m.Publisher, opt => opt.Ignore());
            Mapper.Map(entity, entry);

            entry.Genres.Clear();
            entry.Genres = genres.ToList();

            entry.PlatformTypes.Clear();
            entry.PlatformTypes = platformTypes.ToList();

            entry.Publisher = publisher;

        }

        public void Delete(int id)
        {
            var entry = context.Games.First(m => m.GameId.Equals(id));
            entry.IsDeleted = true;
        }

        public Game Get(System.Linq.Expressions.Expression<Func<Game, bool>> predicate)
        {
            return context.Games.Where(predicate).First(m => !m.IsDeleted);
        }

        public IEnumerable<Game> GetAll()
        {
            return context.Games.Where(m => !m.IsDeleted).ToList();
        }

        public IEnumerable<Game> GetMany(System.Linq.Expressions.Expression<Func<Game, bool>> predicate)
        {
            return context.Games.Where(predicate).Where(m => !m.IsDeleted).ToList();
        }

        public int Count()
        {
            return context.Games.Count(m => !m.IsDeleted);
        }

        public bool IsExists(System.Linq.Expressions.Expression<Func<Game, bool>> predicate)
        {
            return context.Games.Where(predicate).Any(m => !m.IsDeleted);
        }
    }
}
