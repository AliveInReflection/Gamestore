using GameStore.BLL.DTO;
using System.Collections.Generic;
using AutoMapper;
using GameStore.Domain.Entities;
using GameStore.BLL.Infrastructure;
using GameStore.BLL.Interfaces;
using GameStore.DAL.Interfaces;
using GameStore.DAL.Concrete;
using System;

namespace GameStore.BLL.Services
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork database;

        public CommentService(IUnitOfWork database)
        {
            this.database = database;
        }

        public void Create(string gameKey, CommentDTO comment)
        {
            if (comment == null)
            {
                throw new ValidationException("No content received");
            }

            var gameEntry = database.Games.GetSingle(m => m.GameKey.Equals(gameKey));
            
            Mapper.CreateMap<CommentDTO, Comment>();
            var commentToSave = Mapper.Map<CommentDTO, Comment>(comment);
            
            gameEntry.Comments.Add(commentToSave);

            database.Save();
        }

        public IEnumerable<CommentDTO> Get(string gameKey)
        {
            var game = database.Games.GetSingle(m => m.GameKey.Equals(gameKey));

            var commentEntries = game.Comments;

            Mapper.CreateMap<Comment, CommentDTO>();
            var comments = Mapper.Map<IEnumerable<Comment>, IEnumerable<CommentDTO>>(commentEntries);
            
            return comments;
        }

        public void Dispose()
        {
        }
    }
}
