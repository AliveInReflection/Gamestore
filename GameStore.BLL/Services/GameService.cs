using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Infrastructure;
using GameStore.BLL.Interfaces;
using GameStore.DAL.Interfaces;
using GameStore.Domain.Entities;
using GameStores.DAL.Concrete;

namespace GameStore.BLL.Services
{
    public class GameService : IGameService
    {
        private readonly IUnitOfWork database;

        public GameService()
        {
            database = new UnitOfWork("GameStoreContext");
        }

        public GameService(IUnitOfWork database)
        {
            this.database = database;
        }

        public void AddGame(GameDTO game)
        {
            //if (game == null)
            //    throw new ValidationException("No content received");

            //Mapper.CreateMap<GameDTO, Game>();
            //var gameToSave = Mapper.Map<GameDTO, Game>(game);

            //var entry = database.Games.Get(m => m.GameKey.Equals(gameToSave.GameKey));
            //if (entry != null)
            //    throw new ValidationException("Another game has the same game key");

            Mapper.CreateMap<GameDTO, Game>();
            var gameToSave = Mapper.Map<GameDTO, Game>(game);
            database.Games.Add(gameToSave);
            database.Save();

        }

        public void EditGame(GameDTO game)
        {
            if (game == null)
                throw new ValidationException("No content received");
            
            Mapper.CreateMap<GameDTO, Game>();
            var gameToSave = Mapper.Map<GameDTO, Game>(game);

            var entry = database.Games.Get(m => m.GameId.Equals(gameToSave.GameId));
            if (entry == null)
                throw new ValidationException("Game not found");

            entry = database.Games.Get(m => m.GameKey.Equals(gameToSave.GameKey));
            if (entry == null)
                throw new ValidationException("Another game has the same game key");

            database.Games.Add(gameToSave);
            database.Save();

        }

        public void DeleteGame(int id)
        {
            var entry = database.Games.Get(m => m.GameId.Equals(id));
            if (entry == null)
                throw new ValidationException("Game not found");

            var commentIds = database.Comments.GetMany(m => m.GameId.Equals(id)).Select(m => m.CommentId);
            foreach (var commentId in commentIds)
            {
                database.Comments.Delete(commentId);
            }
            database.Games.Delete(id);
            database.Save();
        }

        public GameDTO GetGameByKey(string key)
        {
            var entry = database.Games.Get(m => m.GameKey.Equals(key));
            if (entry == null)
                throw new ValidationException("Game not found");

            Mapper.CreateMap<Game, GameDTO>();
            var game = Mapper.Map<Game, GameDTO>(entry);
            return game;
        }

        public IEnumerable<GameDTO> GetAllGames()
        {
            Mapper.CreateMap<Game, GameDTO>();
            var games = Mapper.Map<IEnumerable<Game>, IEnumerable<GameDTO>>(database.Games.GetAll());
            return games;
        }

        public void AddComment(CommentDTO comment)
        {
            if (comment == null)
                throw new ValidationException("No content received");

            Mapper.CreateMap<CommentDTO, Comment>();
            var commentToSave = Mapper.Map<CommentDTO, Comment>(comment);
            database.Comments.Add(commentToSave);
            database.Save();
        }

        public IEnumerable<CommentDTO> GetCommentsByGameKey(string key)
        {
            var game = database.Games.Get(m => m.GameKey.Equals(key));
            if (game == null)
                throw new ValidationException("Game not found");

            var commentEntries = database.Comments.GetMany(m => m.GameId.Equals(game.GameId));
            Mapper.CreateMap<Comment, CommentDTO>();
            var comments = Mapper.Map<IEnumerable<Comment>, IEnumerable<CommentDTO>>(commentEntries);
            return comments;
        }

        public IEnumerable<GameDTO> GetGamesByGenre(int genreId)
        {
            var genre = database.Genres.Get(m => m.GenreId.Equals(genreId));
            if (genre == null)
                throw new ValidationException("Genre not found");
            var gameEntries = genre.Games.ToList();
            Mapper.CreateMap<Game, GameDTO>();
            var games = Mapper.Map<IEnumerable<Game>, IEnumerable<GameDTO>>(gameEntries);
            return games;
        }

        public IEnumerable<GameDTO> GetGamesByPlatformTypes(IEnumerable<int> typeIds)
        {
            HashSet<Game> gameEntries = new HashSet<Game>();
            foreach (var typeId in typeIds)
            {
                var platformType = database.PlatformTypes.Get(m => m.PlatformTypeId.Equals(typeId));
                if (platformType == null)
                    throw new ValidationException("Platform type not found");

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
