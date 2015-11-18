using System;
using System.Collections.Generic;
using AutoMapper;
using GameStore.Domain.Entities;
using GameStore.BLL.Infrastructure;
using GameStore.DAL.Interfaces;
using System.Linq;
using System.Text;
using GameStore.Infrastructure.BLInterfaces;
using GameStore.Infrastructure.DTO;
using GameStore.Infrastructure.Enums;

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
                var quote = new StringBuilder();
                quote.Append(BLLConstants.QuoteTagOpen);
                quote.Append(quotedComment.Quote);
                quote.Append(quotedComment.Content);
                quote.Append(BLLConstants.QuoteTagClose);
                commentToSave.Quote =  quote.ToString();
            }

            database.Comments.Create(commentToSave);
            database.Save();
        }

        public IEnumerable<CommentDTO> Get(string gameKey)
        {
            var commentEntries = database.Games.Get(m => m.GameKey.Equals(gameKey)).Comments;

            var comments = Mapper.Map<IEnumerable<Comment>, IEnumerable<CommentDTO>>(commentEntries);
          
            return BuildTree(comments);
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
                comment.Content = BLLConstants.DeleteComment;
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
        private IEnumerable<CommentDTO> BuildTree(IEnumerable<CommentDTO> comments)
        {
            var childComments = comments.Where(m => m.ParentCommentId != null).ToList();

            foreach (var childComment in childComments)
            {
                var parent = comments.First(m => m.CommentId.Equals(childComment.ParentCommentId));
                if (parent.ChildComments == null)
                {
                    parent.ChildComments = new List<CommentDTO>
                    {
                        childComment
                    };
                }
                else
                {
                    parent.ChildComments.Add(childComment);
                }
            }

            return comments.Where(m => m.ParentCommentId == null);
        }

        private bool HasChildren(Comment parent)
        {
            var children = database.Comments.GetMany(m => m.ParentComment.CommentId.Equals(parent.CommentId));
            return children.Any();
        }

        #endregion

    }
}
