using GameStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GameStore.DAL.Interfaces;
using Gamestore.DAL.Context;
using GameStore.DAL.Infrastructure;
using GameStore.DAL.Northwind.Interfaces;

namespace GameStore.DAL.Concrete.Repositories
{
    public class GameRepository : IRepository<Game>
    {
        private GameStoreContext context;
        private INorthwindUnitOfWork northwind;

        public GameRepository(GameStoreContext context, INorthwindUnitOfWork northwind)
        {
            this.context = context;
            this.northwind = northwind;
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
            var gameIdsToExclude = context.Games.Where(m => KeyManager.GetDatabase(m.GameId) == DatabaseType.Northwind).Select(m => m.GameId);
            
            var entry = context.Games.Where(predicate).FirstOrDefault(m => !m.IsDeleted);
            if (entry == null)
            {
                entry = northwind.Games.GetAll(gameIdsToExclude).Where(predicate.Compile()).First();
            }
            else
            {
                BuildGame(entry);
            }
            
            
            return entry;
        }

        public IEnumerable<Game> GetAll()
        {
            var games = context.Games.Where(m => !m.IsDeleted).ToList();
            
            var gameIdsToExclude = context.Games.Where(m => KeyManager.GetDatabase(m.GameId) == DatabaseType.Northwind).Select(m => m.GameId).ToList();

            games.AddRange(northwind.Games.GetAll(gameIdsToExclude));
            
            return games;
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


        private void BuildGame(Game entity)
        {
            var publisher = context.Publishers.First(m => m.PublisherId.Equals(entity.PublisherId));
            var comments = context.Comments.Where(m => m.GameId.Equals(entity.GameId)).ToList();
            
            var genres = context.Genres.Join(context.GameGenre, genre => genre.GenreId, gg => gg.GenreId,(genre,gg) => new {genre = genre, gg = gg})
                .Join(context.Games, m => m.gg.GameId, game => game.GameId, (genre, game) => new { genre = genre, game = game })
                .Where(m => m.game.GameId.Equals(entity.GameId)).Select(m => m.genre.genre).ToList();

            entity.Publisher = publisher;
            entity.Comments = comments;
            entity.Genres = genres;

        }
    }
}
