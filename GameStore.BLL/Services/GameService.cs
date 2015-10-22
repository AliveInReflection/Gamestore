using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Infrastructure;
using GameStore.BLL.Interfaces;
using GameStore.DAL.Interfaces;
using GameStore.Domain.Entities;
using GameStore.DAL.Concrete;

namespace GameStore.BLL.Services
{
    public class GameService : IGameService
    {
        private readonly IUnitOfWork database;


        public GameService(IUnitOfWork database)
        {
            this.database = database;
        }

        public void Create(GameDTO game, IEnumerable<int> genreIds, IEnumerable<int> platformTypeIds)
        {
            if (game == null)
            {
                throw new ValidationException("No content received");
            }

            try
            {
                //throws exception when can not find
                var entry = database.Games.GetSingle(m => m.GameKey.Equals(game.GameKey));
                throw new ValidationException("Another game has the same game key");
            }
            catch (InvalidOperationException)
            {
                Mapper.CreateMap<GameDTO, Game>();
                var gameToSave = Mapper.Map<GameDTO, Game>(game);

                gameToSave.Genres = genreIds.Select(id =>
                {
                    var genreEntry = database.Genres.GetSingle(m => m.GenreId.Equals(id));

                    return genreEntry;
                }).ToList();

                gameToSave.PlatformTypes = platformTypeIds.Select(id =>
                {
                    var ptEntry = database.PlatformTypes.GetSingle(m => m.PlatformTypeId.Equals(id));
                    
                    return ptEntry;
                }).ToList();

                database.Games.Create(gameToSave);
                database.Save();
            }
        }

        public void Update(GameDTO game, IEnumerable<int> genreIds, IEnumerable<int> platformTypeIds)
        {
            if (game == null)
            {
                throw new ValidationException("No content received");
            }

            Game entry;
            try
            {
                entry = database.Games.GetSingle(m => m.GameKey.Equals(game.GameKey));
                // if entry and game are different games with the same key
                if (entry.GameId != game.GameId)
                {
                    throw new ValidationException("Another game has the same game key");
                }
     
            }
            catch (InvalidOperationException)
            {
  
            }
            
            entry = database.Games.GetSingle(m => m.GameId.Equals(game.GameId));

            Mapper.CreateMap<GameDTO, Game>();
            var gameToSave = Mapper.Map(game, entry);

            // Updating game genres
            var existedGenreIds = gameToSave.Genres.Select(m => m.GenreId);

            var genreIdsToAdd = genreIds.Except(existedGenreIds);

            foreach (var id in genreIdsToAdd)
            {
                var genreEntry = database.Genres.GetSingle(m => m.GenreId.Equals(id));
                gameToSave.Genres.Add(genreEntry);
            }

            var genreIdsToRemove = existedGenreIds.Except(genreIds);

            foreach (var id in genreIdsToRemove)
            {
                var genreEntry = database.Genres.GetSingle(m => m.GenreId.Equals(id));
                gameToSave.Genres.Remove(genreEntry);
            }

            // Updating game platform types
            var existedPtIds = gameToSave.PlatformTypes.Select(m => m.PlatformTypeId);

            var ptIdsToAdd = platformTypeIds.Except(existedPtIds);
            
            foreach (var id in ptIdsToAdd)
            {
                var ptEntry = database.PlatformTypes.GetSingle(m => m.PlatformTypeId.Equals(id));
                gameToSave.PlatformTypes.Add(ptEntry);
            }

            var ptIdsToRemove = existedPtIds.Except(platformTypeIds);

            foreach (var id in ptIdsToRemove)
            {
                var ptEntry = database.PlatformTypes.GetSingle(m => m.PlatformTypeId.Equals(id));
                gameToSave.PlatformTypes.Remove(ptEntry);
            }

            database.Games.Update(gameToSave);
            database.Save();
        }

        public void Delete(int gameId)
        {
            var entry = database.Games.GetSingle(m => m.GameId.Equals(gameId));

            var commentIds = entry.Comments.Select(m => m.CommentId);
            foreach (var commentId in commentIds)
            {
                database.Comments.Delete(commentId);
            }
            database.Games.Delete(gameId);
            database.Save();
        }

        public GameDTO Get(string gameKey)
        {
            var entry = database.Games.GetSingle(m => m.GameKey.Equals(gameKey));

            Mapper.CreateMap<Game, GameDTO>();
            var game = Mapper.Map<Game, GameDTO>(entry);
            return game;
        }

        public IEnumerable<GameDTO> GetAll()
        {
            Mapper.CreateMap<Game, GameDTO>();
            var games = Mapper.Map<IEnumerable<Game>, IEnumerable<GameDTO>>(database.Games.GetAll());
            return games;
        }

        public IEnumerable<GameDTO> Get(int genreId)
        {
            var genre = database.Genres.GetSingle(m => m.GenreId.Equals(genreId));

            var gameEntries = genre.Games.ToList();
            Mapper.CreateMap<Game, GameDTO>();
            var games = Mapper.Map<IEnumerable<Game>, IEnumerable<GameDTO>>(gameEntries);
            return games;
        }

        public IEnumerable<GameDTO> Get(IEnumerable<int> platformTypeIds)
        {
            HashSet<Game> gameEntries = new HashSet<Game>();
            foreach (var typeId in platformTypeIds)
            {
                var platformType = database.PlatformTypes.GetSingle(m => m.PlatformTypeId.Equals(typeId));

                var gamesTMP = platformType.Games.ToList();
                foreach (var game in gamesTMP)
                {
                    gameEntries.Add(game);
                }
            }
            Mapper.CreateMap<Game, GameDTO>();
            var games = Mapper.Map<IEnumerable<Game>, IEnumerable<GameDTO>>(gameEntries);
            return games;
        }
    }
}
