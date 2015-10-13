using System;
using System.Collections.Generic;
using GameStore.BLL.DTO;
using GameStore.BLL.Interfaces;
using GameStores.DAL.Concrete;
using GameStore.DAL.Interfaces;

namespace GameStore.BLL.Services
{
    public class GameService : IGameService
    {
        private IUnitOfWork database;

        public GameService(IUnitOfWork database)
        {
            this.database = database;
        }

        public void AddGame(GameDTO game)
        {
            throw new NotImplementedException();
        }

        public void EditGame(GameDTO game)
        {
            throw new NotImplementedException();
        }

        public void DeleteGame(int id)
        {
            throw new NotImplementedException();
        }

        public GameDTO GetGameByKey(string key)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GameDTO> GetAllGames()
        {
            throw new NotImplementedException();
        }

        public void AddComment(CommentDTO comment)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CommentDTO> GetCommentsByGameKey(string key)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GameDTO> GetGamesByGenre(int genreId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GameDTO> GetGamesByPlatformTypes(IEnumerable<int> typeIds)
        {
            throw new NotImplementedException();
        }
    }
}
