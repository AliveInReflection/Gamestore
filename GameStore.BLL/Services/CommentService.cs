using System;
using System.Collections.Generic;
using AutoMapper;
using GameStore.Domain.Entities;
using GameStore.BLL.Infrastructure;
using GameStore.DAL.Interfaces;
using System.Linq;
using GameStore.Infrastructure.BLInterfaces;
using GameStore.Infrastructure.DTO;

namespace GameStore.BLL.Services
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork database;

        public CommentService(IUnitOfWork database)
        {
            this.database = database;
        }

        public void Create(CommentDTO comment)
        {
            if (comment == null)
            {
                throw new NullReferenceException("No content received");
            }

            var commentToSave = Mapper.Map<CommentDTO, Comment>(comment);

            if (comment.QuoteId != null)
            {
                var quotedComment = database.Comments.Get(m => m.CommentId.Equals(comment.QuoteId.Value));
                commentToSave.Quote = "<quote>" + quotedComment.Quote + quotedComment.Content + "</quote>";
            }

            database.Comments.Create(commentToSave);
            database.Save();
        }

        public IEnumerable<CommentDTO> Get(string gameKey)
        {
            var commentEntries = database.Comments.GetMany(m => m.Game.GameKey.Equals(gameKey));

            var comments = Mapper.Map<IEnumerable<Comment>, IEnumerable<CommentDTO>>(commentEntries);

            foreach (var comment in comments)
            {
                GetChildComments(comment);
            }
            
            return comments;
        }

        public CommentDTO Get(int commentId)
        {
            var entry = database.Comments.Get(m => m.CommentId.Equals(commentId));
            return Mapper.Map<Comment, CommentDTO>(entry);
        }

        public void Delete(int commentId)
        {
            var comment = database.Comments.Get(m => m.CommentId.Equals(commentId));
            if (HasChildren(comment))
            {
                comment.Content = "<Message deleted>";
            }
            else
            {
                database.Comments.Delete(commentId);
            }
            database.Save();
        }

        public void Update(CommentDTO comment)
        {
            if (comment == null)
            {
                throw new NullReferenceException("No content received");
            }

            var commentToSave = Mapper.Map<CommentDTO, Comment>(comment);
            database.Comments.Update(commentToSave);
            database.Save();
        }

        #region privates
        private void GetChildComments(CommentDTO parent)
        {
            var childComments = database.Comments.GetMany(m => m.ParentComment.CommentId.Equals(parent.CommentId));
            parent.ChildComments = Mapper.Map<IEnumerable<Comment>, IEnumerable<CommentDTO>>(childComments);

            foreach (var childComment in parent.ChildComments)
            {
                GetChildComments(childComment);
            }
        }

        private bool HasChildren(Comment parent)
        {
            var children = database.Comments.GetMany(m => m.ParentComment.CommentId.Equals(parent.CommentId));
            return children.Any();
        }

        #endregion

    }
}
