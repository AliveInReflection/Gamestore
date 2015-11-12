using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using GameStore.DAL.Concrete.RepositoryDecorators;
using GameStore.DAL.GameStore.Interfaces;
using GameStore.DAL.Infrastructure;
using GameStore.DAL.Northwind.Interfaces;
using GameStore.Domain.Entities;

namespace GameStore.DAL.Concrete.Repositories
{
    public class GameRepository : BaseRepository<Game>
    {
        public GameRepository(IGameStoreUnitOfWork gameStore, INorthwindUnitOfWork northwind)
            : base(gameStore, northwind)
        {
            
        }

        public override void Create(Game entity)
        {
            BuildEntityToSave(entity);
            gameStore.Games.Create(entity);
        }

        public override void Update(Game entity)
        {
            var database = KeyManager.GetDatabase(entity.GameId);

            if (database == DatabaseType.Northwind && 
                gameStore.Games.Get(m => m.GameId.Equals(entity.GameId)) == null)
            {
                Create(entity);
                return;
            }
            BuildEntityToSave(entity);
            gameStore.Games.Update(entity);
        }

        public override void Delete(int id)
        {
            var database = KeyManager.GetDatabase(id);

            if (database == DatabaseType.Northwind &&
                gameStore.Games.Get(m => m.GameId.Equals(id)) == null)
            {
                var game = northwind.Games.Get(KeyManager.Decode(id));
                game.IsDeleted = true;
                Create(game);
                return;
            }

            gameStore.Games.Delete(id);
        }

        public override Game Get(Expression<Func<Game, bool>> predicate)
        {
            var entry = gameStore.Games.Get(predicate);
            if (entry == null)
            {
                entry = northwind.Games.GetAll(GetGameIdsToExclude()).Where(predicate.Compile()).First();
            }
        
            return entry;
        }

        public override IEnumerable<Game> GetAll()
        {

            var games = gameStore.Games.GetMany(m => !m.IsDeleted).ToList();

            var gamesFromNorthwind = northwind.Games.GetAll(GetGameIdsToExclude());

            games.AddRange(gamesFromNorthwind);
            
            return games;
        }

        public override IEnumerable<Game> GetMany(Expression<Func<Game, bool>> predicate)
        {
            var games = gameStore.Games.GetMany(predicate).Where(m => !m.IsDeleted).ToList();

            var gamesFromNorthwind = northwind.Games.GetAll(GetGameIdsToExclude()).Where(predicate.Compile());

            games.AddRange(gamesFromNorthwind);

            return games;
        }

        public override int Count()
        {
            var gameStoreCount = gameStore.Games.GetMany(m => !m.IsDeleted).Count();
            var northwindCount = northwind.Games.Count(GetGameIdsToExclude());

            return gameStoreCount + northwindCount;
        }

        public override bool IsExists(Expression<Func<Game, bool>> predicate)
        {
            var gameIdsToExclude = GetGameIdsToExclude();
            
            bool gameStoreIsExists = gameStore.Games.GetMany(predicate).Any(m => !m.IsDeleted);
            
            return gameStoreIsExists ? gameStoreIsExists : northwind.Games.GetAll(gameIdsToExclude).Any(predicate.Compile());
        }

        private void BuildEntityToSave(Game entity)
        {
            var platformTypeIds = entity.PlatformTypes.Select(m => m.PlatformTypeId);
            var platformTypes = gameStore.PlatformTypes.GetMany(m => platformTypeIds.Contains(m.PlatformTypeId));
            

            var genreIds = entity.Genres.Select(m => m.GenreId);
            var genres = gameStore.Genres.GetMany(m => genreIds.Contains(m.GenreId)).ToList();

            var genresFromNorthwind = northwind.Genres.GetAll(GetGenreIdsToExclude()).Where(m => genreIds.Contains(m.GenreId));
            foreach (var genre in genresFromNorthwind)
            {
                genre.Games = null;
                gameStore.Genres.Create(genre);
            }

            genres.AddRange(genresFromNorthwind);
            

            var publisher = gameStore.Publishers.Get(m => m.PublisherId.Equals(entity.PublisherId));
            if (publisher == null)
            {
                publisher = northwind.Publishers.Get(KeyManager.Decode(entity.PublisherId));
                publisher.Games.Clear();
            }

            entity.PlatformTypes = platformTypes.ToList();
            entity.Genres = genres;
            entity.Publisher = publisher;
        }
        
        private IEnumerable<int> GetGameIdsToExclude()
        {

            return gameStore.Games.GetAll().Select(m => m.GameId).ToList()
                .Where(m => KeyManager.GetDatabase(m) == DatabaseType.Northwind)
                .Select(m => KeyManager.Decode(m));
        }

        private IEnumerable<int> GetGenreIdsToExclude()
        {

            return gameStore.Genres.GetAll().Select(m => m.GenreId).ToList()
                .Where(m => KeyManager.GetDatabase(m) == DatabaseType.Northwind)
                .Select(m => KeyManager.Decode(m));
        }


    }
}
