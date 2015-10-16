using GameStore.BLL.DTO;
using System.Collections.Generic;
using AutoMapper;
using GameStore.Domain.Entities;
using GameStore.BLL.Infrastructure;
using GameStore.BLL.Interfaces;
using GameStore.DAL.Interfaces;
using GameStore.DAL.Concrete;

namespace GameStore.BLL.Services
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork database;

        public CommentService()
        {

        }

        public CommentService(IUnitOfWork database)
        {
            this.database = database;
        }

        public void AddComment(string gamekey, CommentDTO comment)
        {
            if (comment == null)
                throw new ValidationException("No content received");

            var gameEntry = database.Games.Get(m => m.GameKey.Equals(gamekey));
            if (gameEntry == null)
                throw new ValidationException("Game not found");

            Mapper.CreateMap<CommentDTO, Comment>();
            var commentToSave = Mapper.Map<CommentDTO, Comment>(comment);
            gameEntry.Comments.Add(commentToSave);
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
    }
}
