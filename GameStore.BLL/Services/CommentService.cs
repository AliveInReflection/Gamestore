﻿using GameStore.BLL.DTO;
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
            var userEntry = database.Users.GetSingle(m => m.UserId.Equals(comment.UserId));

            var commentToSave = Mapper.Map<CommentDTO, Comment>(comment);
            commentToSave.User = userEntry;

            if (comment.ParentCommentId != null)
            {
                var commentEntry = database.Comments.GetSingle(m => m.CommentId.Equals(comment.ParentCommentId.Value));
                commentToSave.ParentComment = commentEntry;
            }
            else
            {
                gameEntry.Comments.Add(commentToSave);
            }
            database.Comments.Create(commentToSave);
            database.Save();
        }

        public IEnumerable<CommentDTO> Get(string gameKey)
        {
            var game = database.Games.GetSingle(m => m.GameKey.Equals(gameKey));

            var commentEntries = game.Comments;

            Mapper.CreateMap<Comment, CommentDTO>();
            var comments = Mapper.Map<IEnumerable<Comment>, IEnumerable<CommentDTO>>(commentEntries);

            foreach (var comment in comments)
            {
                GetChildComments(comment);
            }
            
            return comments;
        }

        private void GetChildComments(CommentDTO parent)
        {
            var childComments = database.Comments.GetMany(m => m.ParentComment.CommentId.Equals(parent.CommentId));
            parent.ChildComments = Mapper.Map<IEnumerable<Comment>, IEnumerable<CommentDTO>>(childComments);

            foreach (var childComment in parent.ChildComments)
            {
                GetChildComments(childComment);
            }
        }

    }
}
