using GameStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
            if (entity.GameId == 0)
            {
                entity.GameId = GetId();
            }

            var genreIds = entity.Genres.Select(m => m.GenreId);
            foreach (var genreId in genreIds)
            {
                context.GameGenre.Add(new GameGenre()
                {
                    GameId = entity.GameId,
                    GenreId = genreId
                });
            }

            var platformTypeIds = entity.PlatformTypes.Select(m => m.PlatformTypeId);
            var platformTypes = context.PlatformTypes.Where(m => platformTypeIds.Contains(m.PlatformTypeId));
            entity.PlatformTypes = platformTypes.ToList();

            context.Games.Add(entity);
        }

        public void Update(Game entity)
        {
            var database = KeyManager.GetDatabase(entity.GameId);

            if (database == DatabaseType.Northwind)
            {
                Create(entity);
                return;
            }

            var currentGenreIds = entity.Genres.Select(m => m.GenreId).ToList();
            var previousGenreIds = context.GameGenre.Where(m => m.GameId.Equals(entity.GameId)).Select(m => m.GenreId).ToList();

            var genreIdsToAdd = currentGenreIds.Except(previousGenreIds).ToList();
            var genreIdsToRemove = previousGenreIds.Except(currentGenreIds).ToList();

            foreach (var genreId in genreIdsToAdd)
            {
                context.GameGenre.Add(new GameGenre()
                {
                    GameId = entity.GameId,
                    GenreId = genreId
                });
            }

            var genresToRemove = context.GameGenre.Where(m => m.GameId.Equals(entity.GameId) && genreIdsToRemove.Contains(m.GenreId));

            foreach (var genre in genresToRemove)
            {
                context.GameGenre.Remove(genre);
            }

            var platformTypeIds = entity.PlatformTypes.Select(m => m.PlatformTypeId);
            var platformTypes = context.PlatformTypes.Where(m => platformTypeIds.Contains(m.PlatformTypeId)).ToList();

            var entry = context.Games.First(m => m.GameId.Equals(entity.GameId));
            
            Mapper.Map(entity, entry);

            entry.PlatformTypes.Clear();
            entry.PlatformTypes = platformTypes.ToList();

        }

        public void Delete(int id)
        {
            var database = KeyManager.GetDatabase(id);

            if (database == DatabaseType.Northwind)
            {
                var game = northwind.Games.Get(KeyManager.Decode(id));
                game.IsDeleted = true;
                Create(game);
                return;
            }

            var entry = context.Games.First(m => m.GameId.Equals(id));
            entry.IsDeleted = true;
        }

        public Game Get(Expression<Func<Game, bool>> predicate)
        {
            var gameIdsToExclude = GetGameIdsToExclude();
            
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
            var gameIdsToExclude = GetGameIdsToExclude();
            
            var games = context.Games.Where(m => !m.IsDeleted).ToList();
            foreach (var game in games)
            {
                BuildGame(game);
            }

            var gamesFromNorthwind = northwind.Games.GetAll(gameIdsToExclude);
            foreach (var game in gamesFromNorthwind)
            {
                AddCommentsToGame(game);
            }

            games.AddRange(gamesFromNorthwind);
            
            return games;
        }

        public IEnumerable<Game> GetMany(Expression<Func<Game, bool>> predicate)
        {
            var gameIdsToExclude = GetGameIdsToExclude();

            var games = context.Games.Where(m => !m.IsDeleted).ToList();
            foreach (var game in games)
            {
                BuildGame(game);
            }

            var filteredGames = games.Where(predicate.Compile()).ToList();

            var gamesFromNorthwind = northwind.Games.GetAll(gameIdsToExclude).Where(predicate.Compile());
            foreach (var game in gamesFromNorthwind)
            {
                AddCommentsToGame(game);
            }

            filteredGames.AddRange(gamesFromNorthwind);

            return filteredGames;
        }

        public int Count()
        {
            var gameIdsToExclude = GetGameIdsToExclude();

            var gameStoreCount = context.Games.Count(m => !m.IsDeleted);
            var northwindCount = northwind.Games.GetAll(gameIdsToExclude).Count();

            return gameStoreCount + northwindCount;
        }

        public bool IsExists(Expression<Func<Game, bool>> predicate)
        {
            var gameIdsToExclude = GetGameIdsToExclude();
            
            bool gameStoreIsExists = context.Games.Where(predicate).Any(m => !m.IsDeleted);
            bool northwindIsExists = northwind.Games.GetAll(gameIdsToExclude).Any(predicate.Compile());

            return gameStoreIsExists && northwindIsExists;
        }


        private void BuildGame(Game entity)
        {
            AddPublisherToGame(entity);
            AddGenresToGame(entity);
            AddCommentsToGame(entity);
        }

        private void AddCommentsToGame(Game entity)
        {
            var comments = context.Comments.Where(m => m.GameId.Equals(entity.GameId)).ToList();
            entity.Comments = comments;
        }

        private void AddPublisherToGame(Game entity)
        {
            var database = KeyManager.GetDatabase(entity.PublisherId);
            Publisher publisher;
            
            switch (database)
            {
                case DatabaseType.GameStore:
                    publisher = context.Publishers.First(m => m.PublisherId.Equals(entity.PublisherId));
                    break;
                case DatabaseType.Northwind:
                    publisher = northwind.Publishers.Get(KeyManager.Decode(entity.PublisherId));
                    break;
                default:
                    throw new InvalidOperationException(string.Format("Publisher was not found in all databases (id: {0})", entity.PublisherId));
            }
            entity.Publisher = publisher;
        }

        private void AddGenresToGame(Game entity)
        {
            var genreIds = context.Games.Join(context.GameGenre, 
                                                game => game.GameId, 
                                                gg => gg.GameId,
                                                (game, gg) => new {game = game, gg = gg})
                                        .Where(m => m.game.GameId.Equals(entity.GameId))
                                        .Select(m => m.gg.GenreId).ToList();

            var genreIdsInGameStore = genreIds.Where(m => KeyManager.GetDatabase(m) == DatabaseType.GameStore);
            var genreIdsInNorthwind = genreIds.Where(m => KeyManager.GetDatabase(m) == DatabaseType.Northwind).Select(m => KeyManager.Decode(m));

            var genres = context.Genres.Where(m => genreIdsInGameStore.Contains(m.GenreId)).ToList();
            genres.AddRange(northwind.Genres.GetAll(new int[] { }).Where(m => genreIdsInNorthwind.Contains(KeyManager.Decode(m.GenreId))));

            entity.Genres = genres;
        }

        private IEnumerable<int> GetGameIdsToExclude()
        {
            return context.Games.ToList().Where(m => KeyManager.GetDatabase(m.GameId) == DatabaseType.Northwind).Select(m => KeyManager.Decode(m.GameId));
        }

        private int GetId()
        {
            return context.Games.Select(m => m.GameId).OrderBy(m => m).ToList().Last() + KeyManager.Coefficient;
        }
    }
}
